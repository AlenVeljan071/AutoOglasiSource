using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoOglasiSource.Helpers
{
    public class PickerSelectedIndexChangedBehavior : Behavior<Picker>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(RelayCommand), typeof(PickerSelectedIndexChangedBehavior), null);

        public ICommand Command
        {
            get { return (RelayCommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(Picker picker)
        {
            base.OnAttachedTo(picker);

            picker.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        protected override void OnDetachingFrom(Picker picker)
        {
            base.OnDetachingFrom(picker);

            picker.SelectedIndexChanged -= OnSelectedIndexChanged;
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Command != null && Command.CanExecute(sender))
            {
                Command.Execute(sender);
            }
        }
    }
}
