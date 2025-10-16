public class CrackPlantHealthy : PlantHealthy
{
	public override void Damage(int val)
	{
		base.Damage(val);
		animator.SetInteger("hp", hp);
	}

	private void AfterGrow()
	{
	}
}
