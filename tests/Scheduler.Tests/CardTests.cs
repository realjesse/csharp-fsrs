using FSRS;

namespace Scheduler.Tests;

public class CardTests
{
    [Fact]
    public void Constructor_PreservesProvidedValues()
    {
        var due = new DateTime(2026, 9, 28, 9, 30, 0, DateTimeKind.Local);
        var lastReview = new DateTime(2026, 9, 27, 9, 30, 0, DateTimeKind.Utc);

        var card = new Card(42, State.Review, 2, 3.5f, 6.25f, due, lastReview);

        Assert.Equal(42, card.CardId);
        Assert.Equal(State.Review, card.State);
        Assert.Equal(2, card.Step);
        Assert.Equal(3.5f, card.Stability);
        Assert.Equal(6.25f, card.Difficulty);
        Assert.Equal(due, card.Due);
        Assert.Equal(lastReview, card.LastReview);
        Assert.Equal(DateTimeKind.Local, card.Due.Kind);
    }

    [Fact]
    public void Constructor_AllowsNullOptionalValues()
    {
        var card = new Card(42, State.Learning, null, null, null, DateTime.MinValue, null);

        Assert.Null(card.Step);
        Assert.Null(card.Stability);
        Assert.Null(card.Difficulty);
        Assert.Null(card.LastReview);
    }

    [Fact]
    public void Constructor_RejectsUndefinedState()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Card(42, (State)0, null, null, null, DateTime.MinValue, null));
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(float.NaN)]
    [InlineData(float.PositiveInfinity)]
    [InlineData(float.NegativeInfinity)]
    public void Constructor_RejectsInvalidStability(float stability)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Card(42, State.Review, null, stability, null, DateTime.MinValue, null));
    }

    [Theory]
    [InlineData(0.999f)]
    [InlineData(10.001f)]
    [InlineData(float.NaN)]
    [InlineData(float.PositiveInfinity)]
    [InlineData(float.NegativeInfinity)]
    public void Constructor_RejectsInvalidDifficulty(float difficulty)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Card(42, State.Review, null, null, difficulty, DateTime.MinValue, null));
    }
}
