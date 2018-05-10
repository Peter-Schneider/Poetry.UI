namespace Poetry.UI.TranslationSupport
{
    public interface ITranslationRepositoryCreator
    {
        ITranslationRepository Create(string path);
    }
}