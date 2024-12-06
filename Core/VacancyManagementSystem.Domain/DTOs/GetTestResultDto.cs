namespace VacancyManagementSystem.Domain.DTOs;

public class GetTestResultDto
{
    public int ApplicantId { get; set; }
    public List<GetTestAnswerDto> TestAnswers { get; set; }
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }
}
