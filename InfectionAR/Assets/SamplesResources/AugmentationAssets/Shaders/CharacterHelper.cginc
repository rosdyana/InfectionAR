//===============================================================================
//Copyright (c) 2017 PTC Inc. All Rights Reserved.
//
//Vuforia is a trademark of PTC Inc., registered in the United States and other
//countries.
//===============================================================================

float4x4 _CustomLocalToWorld;
fixed4 _GlowColor;
float _LocalHeight;
float _LocalOffset;
float _FillRatio;
float3 _PlaneNorm;
float _ClipModel;
float _UseCustomTransform;

float3 GetPlanePos() {
	float flippedFillRatio = 1.0 - _FillRatio; //Make 0 = empty and 1 = full;
	float distanceAlongNormal = (flippedFillRatio * _LocalHeight) - (_LocalHeight / 2.0); //Convert from _FillRatio to local distance

	//Multiply by _PlaneNorm twice so that negative directions cancel out, making it so _LocalOffset doesn't get flipped
	float3 center = _LocalOffset * _PlaneNorm * _PlaneNorm;
	float3 planePos = distanceAlongNormal * _PlaneNorm - center;

	return planePos;
}