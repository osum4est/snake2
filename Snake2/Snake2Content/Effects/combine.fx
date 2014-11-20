float ambient;
float4 ambientColor;

Texture mainTexture;
sampler mainSampler = sampler_state {
	texture = <mainTexture>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
	AddressU = mirror;
	AddressV = mirror;
};

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

	float4 mainColor = tex2D(mainSampler, texCoord);
	float4 lightColor = tex2D(lightSampler, texCoord);

	float4  finalColor = mainColor * (1 - ambientColor.a) + ambientColor * mainColor;
		finalColor = finalColor * (1 - lightColor.a) + lightColor * mainColor;
		

	return finalColor;
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
