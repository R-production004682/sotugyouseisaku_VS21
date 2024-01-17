Shader "Custom/Wireframe" {
    Properties{
        [Header(Albedo)]
        [MainColor] _BaseColor("Base Color", Color) = (1.0, 1.0, 1.0, 1.0)
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}

        [Header(NormalMap)]
        [Toggle(_NORMALMAP)] _NORMALMAP("Normal Map�g�p�L��", Int) = 0
        [NoScaleOffset] _BumpMap("Normal Map", 2D) = "bump" {}
        [HideInInspector] _BumpScale("Bump Scale", Float) = 1.0

        [Header(Occlution)]
        [Toggle(_OCCLUSIONMAP)] _OCCLUSIONMAP("Occlusion Map�g�p�L��", Int) = 0
        [NoScaleOffset] _OcclusionMap("Occlusion Map", 2D) = "white" {}
        [HideInInspector] _OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0

        [Header(Metallic and Smoothness)]
        _Smoothness("Smoothness(Map�g�p����Alpha=1�̉ӏ��̒l)", Range(0.0, 1.0)) = 0.0
        [Toggle(_METALLICSPECGLOSSMAP)] _METALLICSPECGLOSSMAP("Metallic and Smoothness Map�g�p�L��", Int) = 0
        _Metallic("Metallic(Map�s�g�p���̂�)", Range(0.0, 1.0)) = 0.0
        [NoScaleOffset] _MetallicGlossMap("Metallic and Smoothnes Map", 2D) = "white" {}

        [Header(Emission)]
        [Toggle(_EMISSION)] _EMISSION("Emission�g�p�L��", Int) = 0
        [HDR] _EmissionColor("Emission Color", Color) = (0.0 ,0.0, 0.0)
        [NoScaleOffset] _EmissionMap("Emission Map", 2D) = "white" {}

        [Header(Wireframe)]
        _WireframeWidth("���C���[�t���[����", Range(1, 50)) = 1
        _WireframeColor("���C���[�t���[���F", Color) = (0.0, 0.0, 1.0, 1.0)
        _WireframeEmissionColor("���C���[�t���[����Emission Color", Color) = (0.0, 0.0, 0.0)

        [Space(10)]
        [KeywordEnum(Off, Front, Back)] _Cull("Cull", Int) = 2
    }

        SubShader{
            Tags {
                "Queue" = "Transparent"
                "RenderType" = "Transparent"
                "RenderPipeline" = "UniversalPipeline"
                "UniversalMaterialType" = "Lit"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            LOD 300
            Cull[_Cull]

            HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            ENDHLSL

            Pass {
                Name "Wireframe"
                Tags { "LightMode" = "UniversalForward" }

                HLSLPROGRAM

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _OCCLUSIONMAP
            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF
            #pragma shader_feature_local_fragment _SPECULAR_SETUP
            #pragma shader_feature_local _RECEIVE_SHADOWS_OFF

            // -------------------------------------
            // Universal Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _SHADOWS_SOFT

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing


            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitForwardPass.hlsl"

            #pragma vertex Vert
            #pragma geometry Geom
            #pragma fragment Frag

            #pragma require geometry


            // ---------------------------------------------------------------------------------------
            // �ϐ��錾
            // ---------------------------------------------------------------------------------------
            float _WireframeWidth;
            half4 _WireframeColor;
            half3 _WireframeEmissionColor;


            // ---------------------------------------------------------------------------------------
            // �\����
            // ---------------------------------------------------------------------------------------
            struct v2g {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 positionWS : TEXCOORD2;
                half3 vertexSH : TEXCOORD3;

#ifdef _NORMALMAP
                half4 tangentWS : TEXCOORD4;
#endif
            };

            struct g2f {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 positionWS : TEXCOORD2;
                half3 vertexSH : TEXCOORD3;
                float3 baryCentricCoords : TEXCOORD4;
#ifdef _NORMALMAP
                half4 tangentWS : TEXCOORD5;
#endif
            };


            // ---------------------------------------------------------------------------------------
            // ���\�b�h
            // ---------------------------------------------------------------------------------------
            /**
             * [�W�I���g���V�F�[�_�[�p]
             * �p�����[�^���W�I���g���b�N�V�F�[�_�[�̕ԋp�l�ƂȂ�TriangleStream�ɒǉ�����
             */
            void SetTriVerticesToStream(g2f param[3], inout TriangleStream<g2f> outStream) {
                [unroll]
                for (int i = 0; i < 3; i++) {
                    // ���C���[�t���[���`��p
                    param[i].baryCentricCoords = float3(i == 0, i == 1, i == 2);

                    outStream.Append(param[i]);
                }

                outStream.RestartStrip();
            }


            // ---------------------------------------------------------------------------------------
            // �V�F�[�_�[�֐�
            // ---------------------------------------------------------------------------------------
            /**
             * ���_�V�F�[�_�[
             */
            v2g Vert(Attributes input) {
                v2g output;

                Varyings varyings = LitPassVertex(input);
                output.uv = varyings.uv;
                output.normalWS = varyings.normalWS;
                output.positionWS = varyings.positionWS;
                output.positionCS = varyings.positionCS;
                output.vertexSH = varyings.vertexSH;

#ifdef _NORMALMAP
                output.tangentWS = varyings.tangentWS;
#endif

                return output;
            }

            /**
             * �W�I���g���V�F�[�_�[
             */
            [maxvertexcount(3)]
            void Geom(triangle v2g inputs[3], inout TriangleStream<g2f> outStream) {
                g2f outputs[3];

                [unroll]
                for (int i = 0; i < 3; i++) {
                    outputs[i].positionWS = inputs[i].positionWS;
                    outputs[i].normalWS = inputs[i].normalWS;
                    outputs[i].positionCS = inputs[i].positionCS;
                    outputs[i].uv = inputs[i].uv;
                    outputs[i].vertexSH = inputs[i].vertexSH;
#ifdef _NORMALMAP
                    outputs[i].tangentWS = inputs[i].tangentWS;
#endif
                }

                SetTriVerticesToStream(outputs, outStream);
            }

            /**
             * �t���O�����g�V�F�[�_�[
             */
            half4 Frag(g2f input) : SV_Target {
                Varyings varyings = (Varyings)0;
                varyings.positionCS = input.positionCS;
                varyings.uv = input.uv;
                varyings.positionWS = input.positionWS;
                varyings.normalWS = input.normalWS;
                varyings.vertexSH = input.vertexSH;
#ifdef _NORMALMAP
                varyings.tangentWS = input.tangentWS;
#endif

                SurfaceData surfaceData;
                InitializeStandardLitSurfaceData(input.uv, surfaceData);

                InputData inputData;
                InitializeInputData(varyings, surfaceData.normalTS, inputData);
                inputData.vertexLighting = VertexLighting(inputData.positionWS, inputData.normalWS);


                /* ���C���[�t���[�� */
                // fwidth��1�s�N�Z���̕ω��ʂ����߁A���C���[��`���l���Z�o
                float3 thOfJudgingSides = fwidth(input.baryCentricCoords) * _WireframeWidth * 0.5;

                // �������̃s�N�Z���̍��W��_WireframeWidth�s�N�Z�����ő�������l��菬�����ꍇ�͕`��Ώ�
                // �i�d�S���W�n�ł͒��_����������̕ӂɌ������č��W��1��0�ƕω����邱�Ƃ𗘗p�j
                float3 isOnSides = 1.0 - pow(saturate(input.baryCentricCoords / thOfJudgingSides), 4.0);
                float isOnSide = max(max(isOnSides.x, isOnSides.y), isOnSides.z);

                surfaceData.albedo = lerp(surfaceData.albedo, _WireframeColor.rgb, isOnSide);
                surfaceData.alpha = lerp(surfaceData.alpha, _WireframeColor.a, isOnSide);
                surfaceData.emission = lerp(surfaceData.emission, _WireframeEmissionColor.rgb, isOnSide);

                half4 color = UniversalFragmentPBR(inputData, surfaceData);

                clip(color.a <= 0 ? -1 : 1);

                return color;
            }
            ENDHLSL
        }
        }

            FallBack "Universal Render Pipeline/Lit"
}