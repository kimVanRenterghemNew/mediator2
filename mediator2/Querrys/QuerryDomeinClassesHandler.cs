using mediator2.Controllers;
using mediator2.dom;

namespace mediator2.Querrys;

public class QuerryDomeinClassesHandler : Requist<QuerryDomeinClasses, Response>, RequistValidator<QuerryDomeinClasses>
{
    public Response Handle(QuerryDomeinClasses query)
    {
        return new DomeinClass("naam", 6, 9).ToResponse();
    }

    bool RequistValidator<QuerryDomeinClasses>.Handle(QuerryDomeinClasses query)
    {
        return query.Id >= 1;
    }
}