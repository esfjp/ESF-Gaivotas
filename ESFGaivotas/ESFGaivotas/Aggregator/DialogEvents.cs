using ESFGaivotas.Model.Repository;
using Prism.Events;

namespace ESFGaivotas.Aggregator
{
    public class CreateUserDialogEvent : PubSubEvent<UserModel> { }
}
