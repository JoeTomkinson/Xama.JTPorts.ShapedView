# Xamarin.Android ShapedView
[![platform](https://img.shields.io/badge/platform-Xamarin.Android-purple.svg)](https://www.xamarin.com/)
[![API](https://img.shields.io/badge/API-11%2B-brightgreen.svg?style=flat)](https://android-arsenal.com/api?level=11)
[![License: Apache](https://img.shields.io/github/license/DigitalSa1nt/Xama.JTPorts.ShapedView)](https://opensource.org/licenses/Apache-2.0)

### Namespace: Xama.JTPorts.ShapedView
Port of [ShapeOfView](https://github.com/florent37/ShapeOfView) originally created by [Florent37](https://github.com/florent37)

> Give a custom shape to any android view. Useful for Material Design 2

#### Migrated to use the newer AndroidX Support Libraries
This solution only references AndroidX support libraries, and as such you may need to add this [Nuget Package](https://www.nuget.org/packages/Xamarin.AndroidX.Migration/1.0.4?_src=template) called [Xamarin.AndroidX.Migration](https://github.com/xamarin/XamarinAndroidXMigration) into your Xamarin.Android Solution until you have migrated across fully to AndroidX.

[CREATE SOME SAMPLE GIFS]



# Download

Nuget Package: [CREATE NUGET PACKAGE]

# Use implemented shapes

ViewShape **disable** the `background` property of your view, 
please specify a child with a background to enable it 
```xml
<Xama.JTPorts.ShapedView.Shapes.CircleView
   <FrameLayout
      android:background="@color/blue"
```

ViewShape came with pre-created shapes :

## Circle

[![screen](IMAGE)](LINK)

```xml
<Xama.JTPorts.ShapedView.Shapes.CircleView
        android:layout_width="150dp"
        android:layout_height="150dp"

        android:elevation="4dp"
        app:shape_circle_borderColor="@android:color/black"
        app:shape_circle_borderWidth="2dp">

            <!-- YOUR CONTENT -->

</Xama.JTPorts.ShapedView.Shapes.CircleView>
```

## RoundRect

[![screen](IMAGE)](LINK)

```xml
<Xama.JTPorts.ShapedView.Shapes.RoundRectView
        android:layout_width="150dp"
        android:layout_height="100dp"
        android:elevation="4dp"
        app:shape_roundRect_bottomLeftRadius="10dp"
        app:shape_roundRect_bottomRightRadius="10dp"
        app:shape_roundRect_topLeftRadius="10dp"
        app:shape_roundRect_topRightRadius="10dp"
        
        app:shape_roundRect_borderColor="@android:color/black"
        app:shape_roundRect_borderWidth="2dp"
        >


            <!-- YOUR CONTENT -->

</Xama.JTPorts.ShapedView.Shapes.RoundRectView>
```

## ClipCorner

[![screen](IMAGE)](LINK)

```xml
<Xama.JTPorts.ShapedView.Shapes.CutCornerView
        android:id="@+id/clipCorner"
        android:layout_width="150dp"
        android:layout_height="100dp"
        android:elevation="4dp"
        app:shape_cutCorner_bottomRightSize="20dp">

         <!-- YOUR CONTENT -->

</Xama.JTPorts.ShapedView.Shapes.CutCornerView>
```

## Arc

[![screen](IMAGE)](LINK)

```xml
<Xama.JTPorts.ShapedView.Shapes.ArcView
        android:layout_width="150dp"
        android:layout_height="100dp"
        android:elevation="4dp"
        app:shape_arc_cropDirection="outside"
        app:shape_arc_height="20dp"
        app:shape_arc_position="bottom"
        >

         <!-- YOUR CONTENT -->

</Xama.JTPorts.ShapedView.Shapes.ArcView>
```


## Diagonal

[![screen](IMAGE)](LINK)


```xml
<Xama.JTPorts.ShapedView.Shapes.DiagonalView
        android:layout_width="150dp"
        android:layout_height="100dp"
        android:elevation="4dp"
        app:shape_diagonal_angle="10"
        app:shape_diagonal_direction="right" 
        app:shape_diagonal_position="bottom">

         <!-- YOUR CONTENT -->

</Xama.JTPorts.ShapedView.Shapes.DiagonalView>
```

## Triangle

[![screen](IMAGE)](LINK)

```xml
<Xama.JTPorts.ShapedView.Shapes.TriangleView
         android:layout_width="150dp"
         android:layout_height="150dp"
         android:elevation="4dp"

         app:shape_triangle_percentBottom="0.5"
         app:shape_triangle_percentLeft="0"
         app:shape_triangle_percentRight="0">

            <!-- YOUR CONTENT -->

</Xama.JTPorts.ShapedView.Shapes.TriangleView>
```

## Bubble

[![screen](IMAGE)](LINK)

```xml
<Xama.JTPorts.ShapedView.Shapes.BubbleView
        android:layout_width="150dp"
        android:layout_height="150dp"
        app:shape_bubble_arrowHeight="10dp"
        app:shape_bubble_arrowWidth="10dp"
        app:shape_bubble_arrowPosition="bottom"
        app:shape_bubble_borderRadius="20dp"
        app:arrow_posititon_percent="0.5"
        >

         <!-- YOUR CONTENT -->

</Xama.JTPorts.ShapedView.Shapes.BubbleView>
```

## Star

[![screen](IMAGE)](LINK)

```xml
<Xama.JTPorts.ShapedView.Shapes.StarView
        android:layout_width="150dp"
        android:layout_height="150dp"
        app:shape_star_noOfPoints="5">

         <!-- YOUR CONTENT -->

</Xama.JTPorts.ShapedView.Shapes.StarView>
```

## Polygon

[![screen](IMAGE)](LINK)

```xml
 <Xama.JTPorts.ShapedView.Shapes.PolygonView
            android:layout_width="150dp"
            android:layout_height="100dp"
            app:shape_polygon_noOfSides="9"
            >
         <!-- YOUR CONTENT -->

</Xama.JTPorts.ShapedView.Shapes.PolygonView>
```

## Dotted Edges with Cut Corners

[![screen](IMAGE)](LINK)

```xml
 <Xama.JTPorts.ShapedView.Shapes.DottedEdgesCutCornerView
             android:layout_width="100dp"
             android:layout_height="match_parent"
             app:shape_dot_radius="3dp"
             app:shape_dot_spacing="2dp"
             app:shape_edge_position="right|left"
             app:shape_dottedEdgesCutCorner_bottomLeftSize="8dp"
             app:shape_dottedEdgesCutCorner_bottomRightSize="8dp"
             >

             <!-- YOUR CONTENT -->

 </Xama.JTPorts.ShapedView.Shapes.DottedEdgesCutCornerView>
```

## Animation

All shapes methods can be animated

For example, you can animate a RoundRect corner:

[![screen](gif)](LINK)

```kotlin
ValueAnimator.ofFloat(0f, 200f, 0f).apply {
     addUpdateListener { animation -> roundRect.bottomLeftRadius = (animation.animatedValue as Float).toInt() }
     duration = 800
     repeatCount = ValueAnimator.INFINITE
     repeatMode = ValueAnimator.REVERSE
}.start()
```

# Create you own shape

You can use custom shape to cut your view

# Using Drawable (no elevation)

[![screen](IMAGE)](LINK)

```xml
<Xama.JTPorts.ShapedView.ViewShape
        android:layout_width="100dp"
        android:layout_height="100dp"

        app:shape_clip_drawable="@drawable/YOUR_DRAWABLE"
        >

    <!-- YOUR CONTENT -->

 </Xama.JTPorts.ShapedView.ViewShape>
```

# Using Path (with elevation)

This method generates also a **shadow path** (with Lollipop elevation API 21+)

Wrap your view with a `ViewShape`

```xml
<Xama.JTPorts.ShapedView.ViewShape
        android:id="@+id/myShape"
        android:layout_width="30dp"
        android:layout_height="15dp"
        android:elevation="6dp">

        <!-- YOUR CONTENT -->

 </Xama.JTPorts.ShapedView.ViewShape>
```

Then generate a path in your code :

```java
ViewShape ViewShape = findViewById(R.id.myShape)
ViewShape.setClipPathCreator(new ClipPathManager.ClipPathCreator() {
       @Override
       public Path createClipPath(int width, int height) {
           final Path path = new Path();

            //eg: triangle
           path.moveTo(0, 0);
           path.lineTo(0.5 * width, height);
           path.lineTo(width, 0);
           path.close();

           return path;
       }
});
```

In some case you have to specify `requiresBitmap = true` to enable ViewShape to draw the shape inside a bitmap before clipping your view. It will be less efficient but can make your custom shape work.

# Contribute

Feel free to fork this project, and add customs shapes just as you would do for the original version.

Please then make a `merge-request` after updated the README with a sample of your shape, including a preview

# Current ToDo

- [ ] Tidy up / Finish this readme.
- [ ] Finish port of shapes.
- [ ] Create Nuget Package.
- [ ] See if we can't Csharp it's implementation a bit more.

# Version History

**1.0.1** - Added/Amended missing nugetspec configurations.
**1.0.0** - First port of ShapeOfView version **1.4.5** 

# Credits

Author of ShapeOfView: [Florent Champigny](https://github.com/florent37) - This was ported from his initial Java library.

License
--------

    Copyright 2020 Joe Tomkinson.

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
