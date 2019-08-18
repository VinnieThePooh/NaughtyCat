namespace Plumsail.NaughtyCat.Common.Interfaces
{
    public interface IAuditor
    {
        void AuditUpdateEvent(IAuditable entity);
        void AuditCreateEvent(IAuditable entity);
    }
}
