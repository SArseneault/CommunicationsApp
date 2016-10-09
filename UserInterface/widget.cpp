#include "widget.h"


widget widget::GetInstance()
{
    //Create a new instance if it doesn't exist
    if( Widget == NULL)
    {
        Widget = new widget();

       //Creating a scene
       Scene = new QGraphicsScene();

       //Creating a view to visualize the scene
       View = new QGraphicsView(Scene);
    }

    return Widget;
}

QGraphicsScene widget::getScene()
{
    return Scene;
}

QGraphicsView widget::getView()
{
    return View;
}
