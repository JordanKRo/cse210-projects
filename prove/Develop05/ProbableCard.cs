public class ProbableCard : Card {
    private readonly List<float> numeric_probabilities;

    public readonly float heart_probability;
    public readonly float spade_probability;
    public readonly float diamond_probability;
    public readonly float club_probability;

    public readonly float king_probability;
    public readonly float queen_probability;
    public readonly float jack_probability;

    public readonly float ace_probability;

    public ProbableCard(
        List<float> numericProbabilities,
        float heartProbability,
        float spadeProbability,
        float diamondProbability,
        float clubProbability,
        float kingProbability,
        float queenProbability,
        float jackProbability,
        float aceProbability
    ) {
        numeric_probabilities = numericProbabilities;
        heart_probability = heartProbability;
        spade_probability = spadeProbability;
        diamond_probability = diamondProbability;
        club_probability = clubProbability;
        king_probability = kingProbability;
        queen_probability = queenProbability;
        jack_probability = jackProbability;
        ace_probability = aceProbability;
    }
}