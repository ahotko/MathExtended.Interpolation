# MathExtended.Interpolation
Pure C# Interpolation (2D) Library (Playground). It is a C# rewrite of my old Delphi Interpolation Library.

## Legal information and credits

MathExtended.Interpolation is project by Ales Hotko and was first released in April 2018. It's licensed under the MIT license.

## Usage

```csharp
using MathExtended.Interpolations;

var interpolation = new Interpolation();
interpolation.Add(1, 2);
interpolation.Add(5, 8);
interpolation.Add(7.7, 5);
interpolation.Add(10, 15);
interpolation.Add(11, 11.3);

//Linear interpolation
double interpolatedValueLinear = interpolation.Linear(3.5);
//...or...
interpolation.Linear();
double interpolatedValueLinear = interpolation.Interpolate(3.5);

//Spline interpolation
double interpolatedValue = interpolation.Spline(3.5);
//...or...
interpolation.Spline();
double interpolatedValue = interpolation.Interpolate(3.5);	

//Cosine interpolation
double interpolatedValue = interpolation.Cosine(3.5);
//...or...
interpolation.Cosine();
double interpolatedValue = interpolation.Interpolate(3.5);	
```

## Optional parameters and overloads

All classes have overloaded constructors and overloaded `Add` methods, that take:
* Single X and Y value - `Add(double ValueX, double ValueY)`
* Dictionary of X and Y values - `Add(Dictionary<double, double> Values)`
* Arrays of X and Y values `Add(double[] ValuesX, double[] ValuesY)`
