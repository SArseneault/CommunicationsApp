#ifndef PlayerObject_H
#define PlayerObject_H

#include <QGraphicsRectItem>
#include <widget.h>
#include <QObject>

//Public class inheriting from the QGraphicsRect abstract class
class PlayerObject: public QObject, public QGraphicsRectItem
{
    Q_OBJECT
    //Public Member functions
public:
        void keyPressEvent(QKeyEvent * event);

    //Public slot member functions
public slots:
        void spawn();
};

#endif // PlayerObject_H
