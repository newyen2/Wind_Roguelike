using UnityEngine;

public class Archive : MonoBehaviour
{
    public static Archive Instance;
    public GameObject buildRoot, windRoot, buildArchivePrefab, windArchivePrefab;
    public Transform buildContent, windContent;

    void Awake()
    {
        Instance = this;
    }

    public void OpenArchive(GameObject[] builds,CardData[] Winds)
    {
        transform.localPosition = new Vector2(0, 0);
        buildRoot.SetActive(true);
        windRoot.SetActive(true);

        foreach(Transform child in buildContent)
        {
            Destroy(child.gameObject);
        }

        foreach(Transform child in windContent)
        {
            Destroy(child.gameObject);
        }

        foreach(var build in builds)
        {
            GameObject card = Instantiate(buildArchivePrefab, buildContent);
            card.GetComponent<ArchiveBuildPrefab>().changeData(build);
        }

        foreach(var wind in Winds)
        {
            GameObject card = Instantiate(windArchivePrefab, windContent);
            card.GetComponent<ArchiveWindPrefab>().changeData(wind);
        }

        buildRoot.SetActive(false);
        AudioManager.Instance.Play("click");
    }

    public void SwitchToBuild()
    {
        buildRoot.SetActive(true);
        windRoot.SetActive(false);
        AudioManager.Instance.Play("click");
    }

    public void SwitchToWind()
    {
        buildRoot.SetActive(false);
        windRoot.SetActive(true);
        AudioManager.Instance.Play("click");
    }

    public void Close()
    {
        transform.localPosition = new Vector2(0, -9999);
    }
}