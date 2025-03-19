namespace StoryApp.Domain.Models;

public record GeminiResponse(
    List<Candidate> Candidates,
    UsageMetadata UsageMetadata,
    string ModelVersion
);

public record Candidate(
    Content Content,
    string FinishReason,
    CitationMetadata CitationMetadata,
    double AvgLogprobs
);

public record Content(
    List<Part> Parts,
    string Role
);

public record Part(
    string Text
);

public record CitationMetadata(
    List<CitationSource> CitationSources
);

public record CitationSource(
    int StartIndex,
    int EndIndex
);

public record UsageMetadata(
    int PromptTokenCount,
    int CandidatesTokenCount,
    int TotalTokenCount,
    List<TokenDetail> PromptTokensDetails,
    List<TokenDetail> CandidatesTokensDetails
);

public record TokenDetail(
    string Modality,
    int TokenCount
);
