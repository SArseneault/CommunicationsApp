#ifndef WIDGET_H
#define WIDGET_H

#include <QGraphicsScene>
#include <QGraphicsView>

class widget
{
    //Private references
private:
     static widget Widget;

     //Private constructors
private:
     widget(){}

public:
    //Public fields
    QGraphicsScene Scene;
    QGraphicsView View;

    //Public methods
public:
    static widget GetInstance();
    QGraphicsScene getScene();
    QGraphicsView getView();

};

#endif // WIDGET_H
