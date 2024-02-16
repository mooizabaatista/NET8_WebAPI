using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MbStore.Domain.Entities.Base;

public abstract class EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

	private DateTime? _createdAt;

	public DateTime? CreatedAt
	{
		get { return _createdAt; }
		set { _createdAt = value ?? DateTime.Now; }
	}

    public DateTime? UpdatedAt { get; set; }
}
