using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class SceneController : Singleton<SceneController>
{
	//Usado para indicar que começou a fazer a transição entre as scenes.
	private bool isLoading;
	//Usado para guarda o nome da scene de destino.
	private string targetSceneName;
	//Usado para indicar o tempo mínimo que a tela de loading deve se manter ativa.
	//Caso o valor seja 0 o tempo mínimo será o tempo de carregamento da nova cena.
	private float minLoadingTime;
	void Start()
	{
		
		if (base.SingletonStart ()) {
			isLoading = false;
			targetSceneName = "";
			minLoadingTime = 1;
		}
	}
	//Retorna um IEnumerator para ser utilizado no StartCoroutine, isso serve para que seja possível parar a execução
	//somente deste método durante um determinado período de tempo e voltar da onde parou. Como pode ser visto mais adiante.
	public IEnumerator StartLoadScene()
	{
		//Indica que começou a carregar e com isso evita de ficar chamando esse método repetidamente
		isLoading = true;
		GameObject fadeImage = GameObject.Find("FadeImage") as GameObject;
		if (fadeImage == null) {
			Debug.LogError ("Objeto FadeImage não foi encontrado");
			yield break;
		}
		//Ativa a Trigger "show" do objeto FadeImage indicando que a animação "FadeIn" deve ser iniciada
		fadeImage.GetComponent<Image> ().enabled = true;
		fadeImage.GetComponent<Animator>().SetTrigger("Out");
		//Interrompe a execução até o final do frame, isso é feito para que dê tempo para que a animação seja trocada.
		//antes de executar o resto do código.
		yield return new WaitForEndOfFrame();

		//Se estiver rodando a animação FadeIn indica para esperar a quantidade de segundos que a animação dura.
		if (fadeImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fadeOut"))
			yield return new WaitForSeconds(fadeImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

		//Começa a carregar a cena de loading de forma assíncrona, com isso a execução do jogo não fica travada.
		AsyncOperation op = SceneManager.LoadSceneAsync("LoadingScene");

		//Espera até que a cena de loading termine de carregar. OBS: A cena de loading deve ser leve para que seja
		//carregada rápida.
		while (!op.isDone)
			yield return new WaitForEndOfFrame();
		
		fadeImage = GameObject.Find ("FadeImage"); 
		GameObject foguete = GameObject.Find ("FogueteA") as GameObject;

		fadeImage.GetComponent<Animator> ().SetTrigger ("In");

		yield return new WaitForEndOfFrame();
		if (foguete.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("fogueteIn"))
			yield return new WaitForSeconds(foguete.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
		//Utilizado para garantir que a tela de load vai ficar ativa durante o tempo mínimo que foi definido.
		float timeLoading = Time.time + minLoadingTime;

		//Começa a carregar a cena indicada como destino de forma assíncrona.
		op = SceneManager.LoadSceneAsync(targetSceneName);

		//Utilizado para que não ative a cena automaticamente após terminar de carregar, isso é feito para que possamos
		//mostrar a barra de progresso e porcentagem do carregamento.
		op.allowSceneActivation = false;

		//Enquanto não terminou de carregar, repete esses comandos frame a frame.
		while (!op.isDone)
		{
			//Usado para calcular e guardar a porcentagem de carregamento da nova scene.
			//Obs: o op.progress retorna um valor de 0 até 1 indicando a porcentagem de carregamento e a ativação da scene,
			//porém quando utilizamos o op.allowSceneActivation = false. Ele carrega a scene mas não chega ativar.
			//Nesse caso quando ele retorna o valor 0.9 indica que a cena foi totalmente carregada e que devemos ativar ela.
			//Por isso o valor é dividido por 0.9 para determinar a porcentagem. 
			float percent = (op.progress / 0.9f) * 100;
			//Atualiza os valores na scene de Loading para mostrar a porcentagem em forma de texto e preencher a barra.
			GameObject.Find("BarraDeProguesso").GetComponent<Slider>().value = percent;
			//GameObject.Find("Percent").GetComponent<Text>().text = String.Format("{0:0}", percent) + "%";

			//Quando a porcetagem chegar a 100 para de repetir.
			if (percent == 100)
				break;

			//Usado para esperar até o outro frame antes de repetir os comandos.
			yield return new WaitForEndOfFrame();
		}
		fadeImage = GameObject.Find ("FadeImage"); 
		fadeImage.GetComponent<Animator> ().SetTrigger ("Out");
		foguete.GetComponent<Animator> ().SetTrigger ("Out");
		yield return new WaitForEndOfFrame();
		if (foguete.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("fogueteOut"))
			yield return new WaitForSeconds(foguete.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
		//Caso tenha carregado a cena mais rápido que o tempo mínimo que a tela de Loading deve ficar ativa 
		//espera até passar o tempo desejado.

		while (Time.time < timeLoading)
			yield return new WaitForEndOfFrame();

		//A scene já foi totalmente carregada e aqui indica para o Unity ativar ela.
		op.allowSceneActivation = true;

		//Reseta as variáveis que controla se deve ou não carregar uma nova scene, isso é feito para que possamos chamar este
		//método novamente.
		isLoading = false;
		targetSceneName = "";
	}

	//Esse método será chamado por outros scripts para indicar que deve carregar uma nova scene.
	public void LoadScene(string targetSceneName)
	{
		if (!isLoading) {
			this.targetSceneName = targetSceneName;
			StartCoroutine (StartLoadScene ());
		}
	}
}

