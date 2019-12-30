using System.Collections.Generic;
using Xamarin.Forms;

namespace NoteTaker.Client.Helpers
{
    public delegate void DtoChangedEventHandler<TDto>(TDto dto);

    public class BindableObject<TDto>
    {
        private readonly Dictionary<string, Entry> _bindedEntries = new Dictionary<string, Entry>();
        private readonly Dictionary<string, Editor> _bindedEditors = new Dictionary<string, Editor>();

        public void UpdateObject(TDto dto)
        {
            Dto = dto;
            SetValuesToComponents();
        }

        public event DtoChangedEventHandler<TDto> OnDtoChanged;

        public TDto Dto { get; private set; }

        public void Bind(string propertyName, Entry entry)
        {
            _bindedEntries[propertyName] = entry;
            entry.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                if (Dto != null)
                {
                    Dto.GetType().GetProperty(propertyName).SetValue(Dto, e.NewTextValue);

                    OnDtoChanged?.Invoke(Dto);
                }
            };

            SetValuesToComponents();
        }

        public void Bind(string propertyName, Editor editor)
        {
            _bindedEditors[propertyName] = editor;
            editor.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                if (Dto != null)
                {
                    Dto.GetType().GetProperty(propertyName).SetValue(Dto, e.NewTextValue);

                    OnDtoChanged?.Invoke(Dto);
                }
            };

            SetValuesToComponents();
        }

        private void SetValuesToComponents()
        {
            foreach (var entry in _bindedEntries)
            {
                if (Dto?.GetType()?.GetProperty(entry.Key)?.GetValue(Dto) == null)
                {
                    entry.Value.Text = "";
                }
                else
                {
                    entry.Value.Text = Dto.GetType().GetProperty(entry.Key).GetValue(Dto).ToString();
                }
            }

            foreach (var editor in _bindedEditors)
            {
                if (Dto?.GetType()?.GetProperty(editor.Key)?.GetValue(Dto) == null)
                {
                    editor.Value.Text = "";
                }
                else
                {
                    editor.Value.Text = Dto?.GetType()?.GetProperty(editor.Key)?.GetValue(Dto).ToString();
                }
            }
        }
    }
}
