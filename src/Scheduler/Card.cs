namespace FSRS;

/// <summary>
/// An immutable snapshot of a card's scheduling state.
/// </summary>
public sealed record Card
{
    /// <summary>Creates a card snapshot.</summary>
    /// <param name="cardId">The card's identifier.</param>
    /// <param name="state">The card's current learning state.</param>
    /// <param name="step">The current learning or relearning step, if applicable.</param>
    /// <param name="stability">The card's memory stability, in days, if known.</param>
    /// <param name="difficulty">The card's memory difficulty, if known.</param>
    /// <param name="due">The due date and time.</param>
    /// <param name="lastReview">The previous review date and time, if any.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="state"/> is undefined, or a provided stability or difficulty is outside its valid range.
    /// </exception>
    public Card(
        int cardId,
        State state,
        int? step,
        float? stability,
        float? difficulty,
        DateTime due,
        DateTime? lastReview)
    {
        if (!Enum.IsDefined(state))
        {
            throw new ArgumentOutOfRangeException(nameof(state), state, "State must be a defined value.");
        }

        if (stability is { } stabilityValue && (!float.IsFinite(stabilityValue) || stabilityValue < 0.001f))
        {
            throw new ArgumentOutOfRangeException(nameof(stability), stability, "Stability must be finite and at least 0.001 days.");
        }

        if (difficulty is { } difficultyValue &&
            (!float.IsFinite(difficultyValue) || difficultyValue is < 1f or > 10f))
        {
            throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, "Difficulty must be finite and between 1 and 10.");
        }

        CardId = cardId;
        State = state;
        Step = step;
        Stability = stability;
        Difficulty = difficulty;
        Due = due;
        LastReview = lastReview;
    }

    /// <summary>Gets the card's identifier.</summary>
    public int CardId { get; }

    /// <summary>Gets the card's current learning state.</summary>
    public State State { get; }

    /// <summary>Gets the current learning or relearning step, if applicable.</summary>
    public int? Step { get; }

    /// <summary>Gets the card's memory stability, in days, if known.</summary>
    public float? Stability { get; }

    /// <summary>Gets the card's memory difficulty, if known.</summary>
    public float? Difficulty { get; }

    /// <summary>Gets the due date and time.</summary>
    public DateTime Due { get; }

    /// <summary>Gets the previous review date and time, if any.</summary>
    public DateTime? LastReview { get; }
}
