using Memo.Infrastructure.Commands.Base;
using Memo.Models;
using Memo.ViewModels;

namespace Memo.Infrastructure.Commands
{
    public class FieldClickCommand : Command
    {
        public override void Execute(object parameters)
        {
            object[] param = parameters as object[];
            Field field = param[0] as Field;
            MainViewModel model = param[1] as MainViewModel;
            model.ClickField(field);

        }
    }
}
