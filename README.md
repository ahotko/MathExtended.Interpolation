# MathExtended.Interpolation
Pure C# Interpolation (2D) Library (Playground). It is a C# rewrite of my old Delphi Interpolation Library.

## Legal information and credits

MathExtended.Interpolation is project by Ales Hotko and was first released in April 2018. It's licensed under the MIT license.

## Usage

```csharp
using Data.Annex.MathExtended.Interpolation;

//Linear interpolation
Linear linear = new Linear();
linear.Add(0, 0);
linear.Add(2.5, 2.5);
linear.Add(5, 10);
double result = linear.Interpolate(3.5);

//Parabolic interpolation; parabola between 3 points			
Parabolic parabolic = new Parabolic(-2, 4, 0, 0, 2, 4);
double result = parabolic.Interpolate(3.5);

//Spline interpolation
Spline spline = new Spline();
spline.Add(-6, 2);
spline.Add(2, -4);
spline.Add(6, 6);	
double result = spline.Interpolate(3.5);		
```