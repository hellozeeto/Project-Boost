using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success; 
    [SerializeField] AudioClip crash; 

    [SerializeField] ParticleSystem successParticles; 
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 선언
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if(isTransitioning || collisionDisabled) { return; } // isTranstioning == true랑 같은 뜻 

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    

    void StartSuccessSequence()
    {
        isTransitioning = true; // 착륙을 성공하면 true로 바꿔 사운드가 여러번 나게하지 않는다.
        audioSource.Stop(); //  성공후에도 스페이스바 누르면 부스트소리 나는 문제 해결
        audioSource.PlayOneShot(success); // 성공사운드 구현
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true; // 실패해도 ture로 바꿔 사운드가 여러번 나게하지 않는다.
        audioSource.Stop();
        audioSource.PlayOneShot(crash); // 실패 사운드 구현
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // 다음씬이 없다면 ex)씬의 총 개수는 2이고 마지막 씬의 인덱스는 1이므로 nextSceneIndex ==2이다.
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

   
}
