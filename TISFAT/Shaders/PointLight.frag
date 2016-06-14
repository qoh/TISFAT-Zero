layout(origin_upper_left) in vec4 gl_FragCoord;
#define STEPS 150
#define PASSES 15

uniform vec2 lightPos;
uniform vec3 lightColor;
uniform vec3 ambientColor;
uniform vec3 lightAttenuation;
uniform float lightRadius;

uniform sampler2D s_Texture;
uniform vec2 s_Res;

vec4 plain_blend(vec4 src, vec4 dst)
{
	return vec4(src.a) * src + vec4(1.0f - src.a) * dst;
}

bool isSolid(vec2 pos)
{
	return texture2D(s_Texture, pos).a > 0.0f;
}

/*
vec3 getColor(vec2 pos)
{
	return texture2D(s_Texture, pos).rgb;
} 
*/

bool ray(vec2 a, vec2 b)
{
	vec2 cur = a;
	vec2 inc = (b - a) / float(STEPS);

	for (int i = 0; i < STEPS; i++) {
		if (isSolid(cur))
			return true;

		cur += inc;
	}

	return false;
}
/* 
vec3 getLighting(vec2 pos, vec2 light)
{
	vec2 shadowPos = pos;
	vec2 step = (light - pos) / float(STEPS);
	vec3 black = vec3(0, 0, 0);

	for (int i = 0; i < STEPS; i++)
	{
		if(ray(shadowPos, light))
		{
			return texture2D(s_Texture, pos).rgb * length(pos - light) / vec3(1.0, 1.0, 1.0) + 1. / length(pos - light) * 0.075 * lightColor;
			//return length(pos - light) / vec3(1.0, 1.0, 1.0);
		}

		shadowPos += step;
	}

	return vec3(1.0, 1.0, 1.0) + 1. / length(pos - light) * 0.075 * lightColor;
	//return lightColor;
}

vec3 blendLighting(vec2 pos, vec2 light)
{
	vec3 color = vec3(0, 0, 0);

	for (int i = 0; i <= PASSES; i++)
	{
		color += getLighting(pos, light) / float(PASSES);
	}

	return color;
}
*/

void main()
{
	vec2 pos = gl_FragCoord.xy / s_Res;

	vec4 s_Color = texture2D(s_Texture, pos);

	bool solid = isSolid(pos);

	float zDist = 10.0f;

	if(solid) { zDist = 0.0f; }

	float distance = length(vec3(lightPos - gl_FragCoord, zDist)) / lightRadius;
	float attenuation=1.0 / (lightAttenuation.x + lightAttenuation.y * distance + lightAttenuation.z * distance * distance);

	if(solid)
	{
		//gl_FragColor = vec4(s_Color.rgb * lightColor * attenuation, 1.0f); // No ambient lighting
		gl_FragColor = vec4(s_Color.rgb * (ambientColor + lightColor * attenuation), 1.0f);
		return;
	}

	if (ray(pos, lightPos.xy / s_Res))
		// gl_FragColor = vec4(0.0, 0.0, 0.0, 1.0); // No ambient lighting
		gl_FragColor = vec4(s_Color.rgb * ambientColor, 1.0f);
	else
		// gl_FragColor = vec4(s_Color.rgb * lightColor * attenuation, 1.0f); // No ambient lighting
		gl_FragColor = vec4(s_Color.rgb * (ambientColor + lightColor * attenuation), 1.0f);

}