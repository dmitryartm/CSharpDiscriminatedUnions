# CSlns.DiscriminatedUnions

Discriminated union type generator for C#.

[F# reference on dicscriminated unions](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions)

Discriminated unions provide support for values that can be one of a number of
named cases. Discriminated unions are similar to union types in other languages,
but there are differences. As with a union type in C++ or a variant type in
Visual Basic, the data stored in the value can be one of several distinct
options. Unlike unions in these other languages, however, each of the possible
options is given a case identifier. The case identifiers are names for the
various possible types of values that objects of this type could be.

To generate discriminated union type needs to be
* type needs to be partial
* type need to have `DiscriminatedUnion` attribute
* contain definition of type named `Union`, public properties of which are used
  to define cases.


```c#
[DiscriminatedUnion]
public partial record Result<TOk, TError> {
    private class Union {
        public TOk Ok { get; }
        public TError Error { get; }
    }
}
```

```c#
public void 
```

## Using a Discriminated Union

To define a discriminated union type `Result<TOk, TError>` with two cases `Ok`
of type `TOk`


<table>
<style>
td { vertical-align: top; }
</style>

<tr>
<td style="text-align: center">C#</td>
<td style="text-align: center">F#</td>
</tr>

<tr>
<td colspan="2" style="text-align: center">Type declaration</td>
</tr>

<tr>
<td>

```c#
[DiscriminatedUnion]
public partial record Result<TOk, TError> {
    private record Union(
        TOk Ok,
        TError Error
    );
}
```

</td>
<td>

```f#
type Result<'TOk, 'TError> =
    | Ok of 'TOk
    | Error of 'TError
```

</td>

</tr>


<tr>
<td colspan="2" style="text-align: center">Creating instances</td>
</tr>

<tr>
<td>

```c#
var ok = Result<int, string>.Ok(123);
var error = Result<int, string>.Error("My scary ERROR!");
```

</td>

<td>

```f#
let ok = Result.Ok 123
let error = Result.Error "My scary ERROR!"
```

</td>
</tr>


<tr>
<td colspan="2" style="text-align: center">Matching</td>
</tr>

<tr>
<td>

```c#
result.Switch(
    Ok: value => value,
    Error: error => throw new InvalidOperationException(error)
);
```

<p style="text-align: center;">
Or
</p>

```c#
result.Case switch {
    ResultEnum.Ok => result.GetOk(),
    ResultEnum.Error => throw new InvalidOperationException(result.GetError()),
    _ => throw new ArgumentOutOfRangeException()
};
```

</td>

<td>

```f#
match result with
| Ok ok -> ok
| Error error -> raise (InvalidOperationException error)
```

</td>
</tr>


</table>


<table>
<style>
td { vertical-align: top; }
</style>

<tr>
<td style="text-align: center">C#</td>
<td style="text-align: center">F#</td>
</tr>


<tr>
<td colspan="2" style="text-align: center">Type declaration</td>
</tr>


<tr>
<td>

```c#
[DiscriminatedUnion]
public partial record Shape {
    private record Union(
        double Circle,
        double Square,
        (double Side0, double Side1) Rectangle
    );
}
```

</td>

<td>

```f#
type Shape =
    | Circle of Radius: double
    | Square of Side: double
    | Rectangle of Side0: double * Side1: double
```

</td>
</tr>


<tr>
<td colspan="2" style="text-align: center">Creating instances</td>
</tr>


<tr>
<td>

```c#
var circle = Shape.Circle(10.0);
var square = Shape.Square(5.0);
var rectangle = Shape.Rectangle((2.0, 8.0));
```

</td>

<td>

```f#
let circle = Shape.Circle 10.0
let square = Shape.Square 5.0
let rectangle = Shape.Rectangle (2.0, 8.0)
```

</td>
</tr>

<tr>
<td colspan="2" style="text-align: center">Matching</td>
</tr>

<tr>
<td>

```c#
double GetArea(Shape shape) =>
    shape.Switch(
        Circle: radius => Math.PI * radius * radius,
        Square: side => side * side,
        Rectangle: rectangle => rectangle.Side0 * rectangle.Side1
    );
    
double GetCircleRadius(Shape shape) =>
    shape.Switch(
        Circle: r => r,
        Default: other =>
            throw new InvalidOperationException($"{other} is not a circle")
    );
```

<p style="text-align: center;">
Or
</p>

```c#
double GetArea(Shape shape) {
    switch (shape.Case) {
        case ShapeEnum.Circle:
            var radius = shape.GetCircle();
            return Math.PI * radius * radius;
        case ShapeEnum.Square:
            var side = shape.GetSquare();
            return side * side;
        case ShapeEnum.Rectangle:
            var (side0, side1) = shape.GetRectangle();
            return side0 * side1;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

double GetCircleRadius(Shape shape) =>
    shape.Case switch {
        ShapeEnum.Circle => shape.GetCircle(),
        _ => throw new InvalidOperationException($"{shape} is not a circle") 
    };
```

</td>

<td>

```f#
let GetArea shape =
    match shape with
    | Circle radius -> Math.PI * radius * radius
    | Square side -> side * side
    | Rectangle (side0, side1) -> side0 * side1
    
    
let GetCircleRadius shape =
    match shape with
    | Circle r -> r
    | other -> 
        raise (InvalidOperationException (
            sprintf $"%A{otherShape} is not a circle"))
```

</td>
</tr>

</table>

Type has to be partial and contain definition of type named `Union`, public
properties of which are used to define
union cases.

Type `Union` used only for defining cases, no instances of this type are created
at any point.

Most concise way to define inner type `Union` is to use a record type, but
regular class can be used.

```cs
[DiscriminatedUnion]
public partial record Result<TOk, TError> {
    private class Union {
        public TOk Ok { get; }
        public TError Error { get; }
    }
}
```




