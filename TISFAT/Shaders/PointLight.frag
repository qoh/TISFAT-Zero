layout(origin_upper_left) in vec4 gl_FragCoord;

uniform vec2 lightPos;
uniform vec3 lightColor;
uniform vec3 lightAttenuation;
uniform float lightRadius;

void main()
{
	vec3 fixedLightColor = vec3(1,0,0);
	vec3 fixedLightAttenuation = vec3(1,1,1);

	float distance = length(lightPos - gl_FragCoord) / lightRadius;

	float attenuation=1.0/(fixedLightAttenuation.x+fixedLightAttenuation.y*distance+fixedLightAttenuation.z*distance*distance);
	//gl_FragColor = attenuation * vec4(fixedLightColor,1);
	gl_FragColor = vec4(fixedLightColor, attenuation);
}