namespace FSRS;

/// <summary>
/// The learning state of a card in the FSRS scheduling process.
/// </summary>
public enum State
{

    /// <summary>A card progressing through its initial learning steps.</summary>
    Learning,

    /// <summary>A card in its regular review cycle.</summary>
    Review,

    /// <summary>A card progressing through relearning after a lapse.</summary>
    Relearning
}
