using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputManager
{
	enum Button
	{
		CROSS,
		CIRCLE,
		SQUARE,
		TRIANGLE,
		L_BUMPER,
		R_BUMPER,
		SELECT,
		START,
		UP_DPAD,
		RIGHT_DPAD,
		DOWN_DPAD,
		LEFT_DPAD
	}

	static class Control
	{
		public static float LeftStick_X()
		{
			return Input.GetAxis("Horizontal");
		}

		public static float LeftStick_Y()
		{
			return Input.GetAxis("Vertical");
		}

		public static Vector2 MainStick_2D()
		{
			return new Vector2(LeftStick_X(), LeftStick_Y());
		}

		public static Vector3 MainStick_3D()
		{
			return new Vector3(LeftStick_X(), 0,LeftStick_Y());
		}

		public static float RightStick_X()
		{
			return Input.GetAxis("R_Horizontal");
		}

		public static float RightStick_Y()
		{
			return Input.GetAxis("R_Vertical");
		}

		public static Vector2 SecondStick_2D()
		{
			return new Vector2(RightStick_X(), RightStick_Y());
		}

		public static Vector3 SecondStick_3D()
		{
			return new Vector3(RightStick_X(), 0, RightStick_Y());
		}

		public static bool ButtonDown(Button button)
		{
			return Input.GetButtonDown(button.ToString());
		}

		public static bool ButtonUP(Button button)
		{
			return Input.GetButtonUp(button.ToString());
		}

		public static bool ButtonHold(Button button)
		{
			return Input.GetButton(button.ToString());
		}

		public static bool AnyButton(Button[] ignore = null)
		{
			foreach(Button button in System.Enum.GetValues(typeof(Button)))
			{     
				if (ignore != null)
				{
					for(int i = 0; i < ignore.Length; ++i)
					{
						if(ignore[i] == button)
						{
							return false;
						}
					}
				}

				if (ButtonHold(button)) { return true; }
			}
			return false;
		}

		public static bool AnyLeftAxis()
		{
			if(LeftStick_X() != 0 || LeftStick_Y() != 0)
			{
				return true;
			}
			return false;
		}

		public static bool AnyRightAxis()
		{
			if (RightStick_X() != 0 || RightStick_Y() != 0)
			{
				return true;
			}
			return false;
		}
	}
}
