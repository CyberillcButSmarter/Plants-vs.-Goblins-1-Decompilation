using UnityEngine;

public class PaperZombieHealthy : ZombieHealthy
{
	public AudioClip paperRip;

	[Range(0f, 100f)]
	public int lostPaperRate = 20;

	private bool hasLostPaper;

	public override void Damage(int val)
	{
		base.Damage(val);
		if (!hasLostPaper && Random.Range(0, 100) < lostPaperRate)
		{
			hasLostPaper = true;
			animator.SetTrigger("lostPaper");
			AudioManager.GetInstance().PlaySound(paperRip);
		}
	}
}
