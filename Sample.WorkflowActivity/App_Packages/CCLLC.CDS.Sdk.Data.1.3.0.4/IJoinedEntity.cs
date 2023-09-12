using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{  
    public interface IJoinedEntity
    {
        LinkEntity ToLinkEntity(string searchValue);
    }

    public interface IJoinedEntity<P,E,RE> :IJoinedEntity, IQueryEntity, IQueryEntity<IJoinedEntity<P,E,RE>,RE> where P : IQueryEntity<P,E> where E : Entity where RE : Entity
    {
        IJoinedEntitySettings<P, E, RE> With { get; }        
    }

    public interface IJoinedEntitySettings<P,E,RE> where P : IQueryEntity<P, E> where E : Entity where RE : Entity
    {
        IJoinedEntity<P, E, RE> Alias(string aliasName);
    }
}
