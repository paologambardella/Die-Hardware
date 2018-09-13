using System;

public static class Easing
{
	// copied from:
	// http://theinstructionlimit.com/flash-style-tweeneasing-functions-in-c

	// Adapted from source : http://www.robertpenner.com/easing/
	
	public static float Ease(double linearStep, float acceleration, EasingType type)
	{
		float easedStep = acceleration > 0 ? EaseIn(linearStep, type) : 
			acceleration < 0 ? EaseOut(linearStep, type) : 
				(float) linearStep;
		
		return MathHelper.Lerp(linearStep, easedStep, Math.Abs(acceleration));
	}
	
	public static float EaseIn(double linearStep, EasingType type)
	{
		switch (type)
		{
		case EasingType.Step:       return linearStep < 0.5 ? 0 : 1;
		case EasingType.Linear:     return (float)linearStep;
		case EasingType.Sine:       return Sine.EaseIn(linearStep);
		case EasingType.Quadratic:  return Power.EaseIn(linearStep, 2);
		case EasingType.Cubic:      return Power.EaseIn(linearStep, 3);
		case EasingType.Quartic:    return Power.EaseIn(linearStep, 4);
		case EasingType.Quintic:    return Power.EaseIn(linearStep, 5);
		case EasingType.Back:		return Back.EaseIn(linearStep);
		case EasingType.Bounce:		return Bounce.EaseIn(linearStep,0,1,1);
		}
		throw new NotImplementedException();
	}



	
	public static float EaseOut(double linearStep, EasingType type)
	{
		switch (type)
		{
		case EasingType.Step:       return linearStep < 0.5 ? 0 : 1;
		case EasingType.Linear:     return (float)linearStep;
		case EasingType.Sine:       return Sine.EaseOut(linearStep);
		case EasingType.Quadratic:  return Power.EaseOut(linearStep, 2);
		case EasingType.Cubic:      return Power.EaseOut(linearStep, 3);
		case EasingType.Quartic:    return Power.EaseOut(linearStep, 4);
		case EasingType.Quintic:    return Power.EaseOut(linearStep, 5);
		case EasingType.Back:		return Back.EaseOut(linearStep);
		case EasingType.Bounce:		return Bounce.EaseOut(linearStep,0,1,1);
		}
		throw new NotImplementedException();
	}

	
	public static float EaseInOut(double linearStep, EasingType easeInType, EasingType easeOutType)
	{
		return linearStep < 0.5 ? EaseInOut(linearStep, easeInType) : EaseInOut(linearStep, easeOutType);
	}
	public static float EaseInOut(double linearStep, EasingType type)
	{
		switch (type)
		{
		case EasingType.Step:       return linearStep < 0.5 ? 0 : 1;
		case EasingType.Linear:     return (float)linearStep;
		case EasingType.Sine:       return Sine.EaseInOut(linearStep);
		case EasingType.Quadratic:  return Power.EaseInOut(linearStep, 2);
		case EasingType.Cubic:      return Power.EaseInOut(linearStep, 3);
		case EasingType.Quartic:    return Power.EaseInOut(linearStep, 4);
		case EasingType.Quintic:    return Power.EaseInOut(linearStep, 5);
		case EasingType.Back:		return Back.EaseInOut(linearStep);
		case EasingType.Bounce:		return Bounce.EaseInOut(linearStep,0,1,1);
		}
		throw new NotImplementedException();
	}



	static class Back
	{
		public static float EaseIn(double s)
		{
			return (float)((Math.Pow(s,1.5f)-s*Math.Sin(s*MathHelper.Pi)) ); // correct
		}
		public static float EaseOut(double s)
		{
			s = 1-s;
			return (float)(1-(Math.Pow(s,1.5f)-s*Math.Sin(s*MathHelper.Pi)) ); // correct
		}
		public static float EaseInOut(double s)
		{
			s *= 2;
			if (s < 1) return EaseIn(s)*0.5f;
			return EaseOut((s-1))*0.5f+0.5f;
		}
	}


	
	static class Sine
	{
		public static float EaseIn(double s)
		{
			return (float)Math.Sin(s * MathHelper.HalfPi - MathHelper.HalfPi) + 1;
		}
		public static float EaseOut(double s)
		{
			return (float)Math.Sin(s * MathHelper.HalfPi);
		}
		public static float EaseInOut(double s)
		{
			return (float)(Math.Sin(s * MathHelper.Pi - MathHelper.HalfPi) + 1) / 2;
		}
	}











	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	/// 
	static class Bounce
	{


	public static float EaseOut( double t, double b, double c, double d )
	{
		if ( ( t /= d ) < ( 1 / 2.75 ) )
			return (float)(c * ( 7.5625 * t * t ) + b);
		else if ( t < ( 2 / 2.75 ) )
			return (float)(c * ( 7.5625 * ( t -= ( 1.5 / 2.75 ) ) * t + .75 ) + b);
		else if ( t < ( 2.5 / 2.75 ) )
			return (float)(c * ( 7.5625 * ( t -= ( 2.25 / 2.75 ) ) * t + .9375 ) + b);
		else
			return (float)(c * ( 7.5625 * ( t -= ( 2.625 / 2.75 ) ) * t + .984375 ) + b);
	}
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
		public static float EaseIn( double t, double b, double c, double d )
	{
		return (float)(c - EaseOut( d - t, 0, c, d ) + b);
	}
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
		public static float EaseInOut( double t, double b, double c, double d )
	{
		if ( t < d / 2 )
			return (float)(EaseIn( t * 2, 0, c, d ) * .5 + b);
		else
			return (float)(EaseOut( t * 2 - d, 0, c, d ) * .5 + c * .5 + b);
	}

		
	}




	
	static class Power
	{
		public static float EaseIn(double s, int power)
		{
			return (float)Math.Pow(s, power);
		}
		public static float EaseOut(double s, int power)
		{
			var sign = power % 2 == 0 ? -1 : 1;
			return (float)(sign * (Math.Pow(s - 1, power) + sign));
		}
		public static float EaseInOut(double s, int power)
		{
			s *= 2;
			if (s < 1) return EaseIn(s, power) / 2;
			var sign = power % 2 == 0 ? -1 : 1;
			return (float)(sign / 2.0 * (Math.Pow(s - 2, power) + sign * 2));
		}
	}
}

public enum EasingType
{
	Step,
	Linear,
	Sine,
	Quadratic,
	Cubic,
	Quartic,
	Quintic,
	Back,
	Bounce
}

public static class MathHelper
{
	public const float Pi = (float)Math.PI;
	public const float HalfPi = (float)(Math.PI / 2);
	
	public static float Lerp(double from, double to, double step)
	{
		return (float)((to - from) * step + from);
	}
}