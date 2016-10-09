#ifndef PlayerObject_H
#define PlayerObject_H

#include <QGraphicsRectItem>
#include <widget.h>

//Public class inheriting from the QGraphicsRect abstract class
class PlayerObject: public QGraphicsRectItem
{
    //Public Member functions
public:
        void keyPressEvent(QKeyEvent * event);

};

#endif // PlayerObject_H
