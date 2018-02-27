//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public static class CircleUtility { // as a utility, it's NOT A MONOCLASS!!!
//	//can't add this script to anything, but OTHER SCROPTS can call/reference what's in it
//
//
//	public static Vector3 PointOnCircle(float radius, float angle) { //***???***
//		float angleInRadians = angle * Mathf.Deg2Rad; //mathf.deg2rad is a constant value of 180/pi, (stands for degrees 2 radians)
//		return new Vector3 (radius * Mathf.Cos (angleInRadians), //x value
//			radius * Mathf.Sin (angleInRadians), // y value
//			0f); //z value
//	}//end pub stat v3
//
//	public static Vector2 PointOnSphere (float radius, float horizontalAngle, float verticalAngle) {
//		float horizontalRadians = horizontalAngle * Mathf.Deg2Rad;
//		float verticalRadians = verticalAngle * Mathf.Deg2Rad;
//
//		return new Vector3 (
//			radius * Mathf.Sin(horizontalRadians) * Mathf.Cos(verticalRadians),
//			radius * Mathf.Sin(verticalRadians),
//			radius * Mathf.Cos(horizontalRadians) * Mathf.Cos(verticalRadians));
//	}//end pub stat v2
//}//END SCRIPT
