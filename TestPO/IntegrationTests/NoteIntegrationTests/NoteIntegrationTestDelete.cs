using Logic;

using Model;

using Repositories;

using RepositoriesImplementations;

namespace TestPO.NoteIntegrationTests
{
	[TestClass]
	public class NoteIntegrationTestDelete
	{
		[TestMethod]
		public void Test()
		{
			INoteRepo notesRepo = new NoteFileRepo();
			INoteService notesService = new NoteService(notesRepo);
			var id = Guid.NewGuid();
			Note check1 = new(id, "test1", false);
			notesService.Create(check1);

			notesService.Delete(id);

			Assert.IsNull(notesService.GetNote(id), "AlarmClockDelete");
		}
	}
}
