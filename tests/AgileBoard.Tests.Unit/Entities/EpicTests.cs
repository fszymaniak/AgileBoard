// using AgileBoard.Core.Entities;
// using AgileBoard.Core.Exceptions;
// using AgileBoard.Core.ValueObjects;
// using Shouldly;
// using Xunit;
//
// namespace AgileBoard.Unit.Tests.Entities;
//
// public class EpicTests
// {
//     #region Assert
//
//     private readonly Epic _epic;
//
//     public EpicTests()
//     {
//         _epic = new Epic(Guid.NewGuid(), "name", "status", "description", "acceptanceCriteria", DateTimeOffset.Now);
//     }
//
//     #endregion
//     
//     
//     [Fact]
//     public void given_empty_name_change_epic_throw_exception()
//     {
//         // ARRANGE
//         var emptyName = string.Empty;
//
//         // ACT
//         var exception = Record.Exception(() => _epic.ChangeName(emptyName)); 
//             
//         // ASSERT
//         exception.ShouldNotBeNull();
//         exception.ShouldBeOfType<EmptyNameException>();
//         exception.Message.ShouldBe("Name is empty.");
//     }
//     
//     [Fact]
//     public void given_empty_status_change_epic_throw_exception()
//     {
//         // ARRANGE
//         var emptyStatus = string.Empty;
//
//         // ACT
//         var exception = Record.Exception(() => _epic.ChangeStatus(emptyStatus)); 
//             
//         // ASSERT
//         exception.ShouldNotBeNull();
//         exception.ShouldBeOfType<EmptyStatusException>();
//         exception.Message.ShouldBe("Status is empty.");
//     }
//     
//     [Fact]
//     public void given_empty_description_change_epic_throw_exception()
//     {
//         // ARRANGE
//         var emptyDescription = string.Empty;
//
//         // ACT
//         var exception = Record.Exception(() => _epic.ChangeDescription(emptyDescription)); 
//             
//         // ASSERT
//         exception.ShouldNotBeNull();
//         exception.ShouldBeOfType<EmptyDescriptionException>();
//         exception.Message.ShouldBe("Description is empty.");
//     }
//     
//     [Fact]
//     public void given_description_over_the_limit_change_epic_throw_exception()
//     {
//         // ARRANGE
//         var rand = new Random();
//         var descriptionOverMaxLimit = string.Join("", Enumerable.Repeat(0, 501).Select(n => (char)rand.Next(127)));
//
//         // ACT
//         var exception = Record.Exception(() => _epic.ChangeDescription(descriptionOverMaxLimit)); 
//             
//         // ASSERT
//         exception.ShouldNotBeNull();
//         exception.ShouldBeOfType<DescriptionOverMaxCharLimit>();
//         exception.Message.ShouldBe("Description over the max characters limit.");
//     }
//     
//     [Fact]
//     public void given_empty_acceptance_criteria_change_epic_throw_exception()
//     {
//         // ARRANGE
//         var emptyAcceptanceCriteria = string.Empty;
//
//         // ACT
//         var exception = Record.Exception(() => _epic.ChangeAcceptanceCriteria(emptyAcceptanceCriteria)); 
//             
//         // ASSERT
//         exception.ShouldNotBeNull();
//         exception.ShouldBeOfType<EmptyAcceptanceCriteriaException>();
//         exception.Message.ShouldBe("Acceptance Criteria is empty.");
//     }
//
//     [Fact]
//     public void given_epic_fields_change_should_succeed()
//     {
//         // ARRANGE
//         var updatedName = "updatedName";
//         var updatedStatus = "updatedStatus";
//         var updatedDescription = "updatedDescription";
//         var updatedAcceptanceCriteria = "updatedAcceptanceCriteria";
//         
//         // ACT
//         _epic.ChangeName(updatedName);
//         _epic.ChangeStatus(updatedStatus);
//         _epic.ChangeDescription(updatedDescription);
//         _epic.ChangeAcceptanceCriteria(updatedAcceptanceCriteria);
//         
//         // ASSERT
//         _epic.Name.ShouldBe<Name>(updatedName);
//         _epic.Status.ShouldBe<Status>(updatedStatus);
//         _epic.Description.ShouldBe<Description>(updatedDescription);
//         _epic.AcceptanceCriteria.ShouldBe<AcceptanceCriteria>(updatedAcceptanceCriteria);
//     }
// }