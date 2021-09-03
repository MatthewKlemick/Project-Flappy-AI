using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird
{
    public class PipeControler : MonoBehaviour
    {
        [Header("Ref")]
        [SerializeField] private GameObject pipePrefab = null;

        [Header("Pipe Settings")]
        [SerializeField] private float gapSize = 5f;
        [SerializeField] private float spawnRate = 3f;

        private float spawnConter;
        private readonly List<GameObject> pipes = new List<GameObject>();

        void Update()
        {
            ClearPipes();

            CreatePipes();
        }
        void ClearPipes()
        {
            for (int i = pipes.Count - 1; i >= 0; i--)
            {
                if (pipes[i].transform.position.x < -10f)
                {
                    Destroy(pipes[i].gameObject);

                    pipes.RemoveAt(i);
                }
            }
        }
        void CreatePipes()
        {
            spawnConter -= Time.deltaTime;

            if (spawnConter > 0f) { return; }

            GameObject topPipe = Instantiate(pipePrefab, transform.position, Quaternion.Euler(0f,0f,180f));
            GameObject bottomPipe = Instantiate(pipePrefab, transform.position, Quaternion.identity);

            float centerHeight = UnityEngine.Random.Range(-1.5f, 1.4f);

            topPipe.transform.Translate(Vector3.up * (centerHeight + (gapSize / 2)),Space.World);
            bottomPipe.transform.Translate(Vector3.up * (centerHeight - (gapSize / 2)),Space.World);

            pipes.Add(topPipe);
            pipes.Add(bottomPipe);

            spawnConter = spawnRate;
        }
        public void ClearAllPipes()
        {
            foreach (var pipe in pipes)
            {
                Destroy(pipe.gameObject);
            }

            pipes.Clear();

            spawnConter = 0f;
        }
    }
}
