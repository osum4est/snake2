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

	//lightColor.rgb = clamp(lightColor.rgb / lightColor.a, 0, 1);

	/*lightColor.a = clamp(lightColor.a - 0.05, 0, 1);
	lightColor.r = lightColor.r * lightColor.a;
	lightColor.g = lightColor.g * lightColor.a;
	lightColor.b = lightColor.b * lightColor.a;*/

	/*float4 finalColor = mainColor + mainColor.a * lightColor;
	finalColor = finalColor + finalColor.a * ambientColor;*/

	//lightColor.a = clamp(lightColor.a * ambientColor.a, 0, 1);

	float ambientAmount;

	//if (lightColor.a != 0)
	{
		//ambientAmount = lightColor.a;
	}
	//else
	{
		ambientAmount = ambientColor.a;
	}

	float4  finalColor = mainColor * (1 - ambientColor.a) + ambientColor * mainColor;
	finalColor = finalColor * (1 - lightColor.a) + lightColor * mainColor;
		//finalColor += ambientColor * mainColor;

		//if (lightColor.a == 0)
			
		//else
			//finalColor.rgb = lightColor.rgb + ambientColor.rgb;

	//float4 finalColor;
	/*finalColor.r += ambientColor.r;
	finalColor.g += ambientColor.g;
	finalColor.b += ambientColor.b;*/

	//if (finalColor.a == 0)
	//{
	//	finalColor = mainColor * ambientColor;
	//}
	//finalColor += mainColor * lightColor * ambientColor;

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
