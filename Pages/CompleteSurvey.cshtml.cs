using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;

namespace SurveyApp.Pages
{
    public class CompleteSurveyModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public string SurveyResult { get; set; }
        
        public CompleteSurveyModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task OnGetAsync()
        {
            var userId = User.GetUserId();

            var surveyResult = await _dbContext.CompletedSurveys
                .Where(s => s.UserId == userId)
                .FirstOrDefaultAsync(); 
            
            SurveyResult = surveyResult?.SurveyResult ?? "{}"; 
        }

        public async Task<IActionResult> OnPostSaveAsync(string data)
        {
            var userId = User.GetUserId();

            var surveyResult = await _dbContext.CompletedSurveys
                .Where(s => s.UserId == userId)
                .FirstOrDefaultAsync();
            if (surveyResult != null)
            {
                surveyResult.SurveyResult = data;
            }
            else
            {
                _dbContext.CompletedSurveys.Add(new CompletedSurvey
                {
                    SurveyResult = data,
                    UserId = userId
                });
            }
            await _dbContext.SaveChangesAsync();
            
            return new OkResult();
        }
    }
}