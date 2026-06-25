namespace RichLabel.Maui.Handlers
{
    public static class RichActionHandler
    {
        private static readonly Dictionary<string, Func<string, Task>> _customHandlers =
            new(StringComparer.OrdinalIgnoreCase);

        public static void Register(string type, Func<string, Task> handler)
        {
            if (string.IsNullOrWhiteSpace(type) || handler is null) return;
            _customHandlers[type] = handler;
        }

        public static async Task Handle(string type, string? value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(type)) return;

            try
            {
                if (_customHandlers.TryGetValue(type, out var customHandler))
                {
                    await customHandler(value);
                    return;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"No se pudo inicializar la acción: {type}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"No se pudo ejecutar la acción: {ex.Message}");
            }
        }
    }
}
