using FSRS;

namespace Scheduler.Tests;

public class CardTests
{
    [Fact]
    public void Constructor_PreservesProvidedValues()
    {
        var due = new DateTime(2026, 9, 28, 9, 30, 0, DateTimeKind.Utc);
        var lastReview = new DateTime(2026, 9, 27, 9, 30, 0, DateTimeKind.Utc);

        var card = new Card(42, State.Review, 2, 3.5f, 6.25f, due, lastReview);

        Assert.Equal(42, card.CardId);
        Assert.Equal(State.Review, card.State);
        Assert.Equal(2, card.Step);
        Assert.Equal(3.5f, card.Stability);
        Assert.Equal(6.25f, card.Difficulty);
        Assert.Equal(due, card.Due);
        Assert.Equal(lastReview, card.LastReview);
        Assert.Equal(DateTimeKind.Utc, card.Due.Kind);
    }

    [Fact]
    public void Constructor_AllowsNullOptionalValues()
    {
        var card = new Card(42, State.Learning, null, null, null, DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc), null);

        Assert.Null(card.Step);
        Assert.Null(card.Stability);
        Assert.Null(card.Difficulty);
        Assert.Null(card.LastReview);
    }

    [Fact]
    public void Constructor_RejectsUndefinedState()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Card(42, (State)0, null, null, null, DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc), null));
    }

    [Theory]
    [InlineData(DateTimeKind.Local)]
    [InlineData(DateTimeKind.Unspecified)]
    public void Constructor_RejectsNonUtcDue(DateTimeKind kind)
    {
        var due = DateTime.SpecifyKind(DateTime.MinValue, kind);

        Assert.Throws<ArgumentException>(() =>
            new Card(42, State.Learning, null, null, null, due, null));
    }

    [Theory]
    [InlineData(DateTimeKind.Local)]
    [InlineData(DateTimeKind.Unspecified)]
    public void Constructor_RejectsNonUtcLastReview(DateTimeKind kind)
    {
        var due = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
        var lastReview = DateTime.SpecifyKind(DateTime.MinValue, kind);

        Assert.Throws<ArgumentException>(() =>
            new Card(42, State.Learning, null, null, null, due, lastReview));
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(float.NaN)]
    [InlineData(float.PositiveInfinity)]
    [InlineData(float.NegativeInfinity)]
    public void Constructor_RejectsInvalidStability(float stability)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Card(42, State.Review, null, stability, null, DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc), null));
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
            new Card(42, State.Review, null, null, difficulty, DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc), null));
    }
}
