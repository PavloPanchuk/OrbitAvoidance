using Aspects.Services;
using Aspects.Services.Factories.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class JokeManiaBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI jokeText;
    [SerializeField] private Image jokeImage;
    
    private IJokeManiaService _jokeManiaService;

    [Inject]
    public void Construct(IJokeManiaService jokeManiaService)
    {
        _jokeManiaService = jokeManiaService;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
            ProcessJoke();
    }

    private async void ProcessJoke()
    {
        var joke = await _jokeManiaService.GetJoke();
        SetJokeText(joke.JokeText);
        SetJokeImage(joke.JokeImageTexture);
    }

    private void SetJokeText(string text)
        => jokeText.text = text;

    private void SetJokeImage(Texture2D texture)
        => jokeImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);

    public class Factory : ComponentFactory<JokeManiaBox>
    {
    }
}
