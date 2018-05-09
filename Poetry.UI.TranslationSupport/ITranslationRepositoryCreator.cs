namespace Poetry.UI.TranslationSupport
{
    public interface ITranslationRepositoryCreator
    {
        TranslationRepository Create(string path);
    }
}