using Logic;

using Model;

using Repositories;

using RepositoriesImplementations;

namespace TestPPO
{
	[TestClass]
	public class NoteWithoutMoqTests
	{
		[TestMethod]
		public void NoteTest()
		{
			INoteRepo notesRepo = new NoteFileRepo();
			INoteService notesService = new NoteService(notesRepo);
			var id = Guid.NewGuid();
			
			Note check1 = new(id, "test1", false);
			notesService.Create(check1);
			Assert.IsNotNull(check1, "NoteCreate");

			Note check2 = new(id, "changed body", false);
			notesService.Edit(check2);
			Assert.IsNotNull(check2, "NoteEdit");

			notesService.Delete(id);
			Assert.IsNull(notesService.GetNote(id), "AlarmClockDelete");
		}
	}
}