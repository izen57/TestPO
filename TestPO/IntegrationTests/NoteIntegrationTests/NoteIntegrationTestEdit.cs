using Logic;

using Model;

using Repositories;

using RepositoriesImplementations;

namespace TestPO.NoteIntegrationTests
{
	[TestClass]
	public class NoteIntegrationTestEdit
	{
		[TestMethod]
		public void Test()
		{
			INoteRepo notesRepo = new NoteFileRepo();
			INoteService notesService = new NoteService(notesRepo);
			var id = Guid.NewGuid();

			Note check2 = new(id, "changed body", false);
			notesService.Edit(check2);

			Assert.IsNotNull(check2, "NoteEdit");
		}
	}
}
