namespace FSRS;

/// <summary>
/// The rating given to a card during a review.
/// </summary>
public enum Rating
{
    /// <summary>The card was not recalled.</summary>
    Again = 1,

    /// <summary>The card was recalled with significant difficulty.</summary>
    Hard = 2,

    /// <summary>The card was recalled successfully.</summary>
    Good = 3,

    /// <summary>The card was recalled easily.</summary>
    Easy = 4
}
