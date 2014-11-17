float3 lightColor;
float lightRadius;
float lightStrength;
float2 lightCoords;

float screenWidth;
float screenHeight;

sampler mainSampler : register(s0);

Texture lightTexture;
sampler lightSampler = sampler_state {
	texture = <lightTexture>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
	AddressU = mirror;
	AddressV = mirror;
};

struct PixelShaderInput
{
	float4 TextureCoords: TEXCOORD0;
};

float4 PixelShaderFunction(PixelShaderInput input) : COLOR0
{
	float2 texCoord = input.TextureCoords;

	float2 pixelPosition;
	pixelPosition.x = screenWidth * texCoord.x;
	pixelPosition.y = screenHeight * texCoord.y;

	float4 lightMapColor = tex2D(lightSampler, texCoord);

		float coneAttenuation = saturate(1.0f - length(lightCoords - pixelPosition) / lightRadius) * lightStrength;

	float4 light = float4(lightColor.r, lightColor.g, lightColor.b, coneAttenuation);
	return light;
}

technique Technique1
{
	pass Pass1
	{
		AlphaBlendEnable = TRUE;
		SrcBlend = SRCALPHA;
		DestBlend = INVSRCALPHA;

		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}