using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;


public class EnemyPSO : MonoBehaviour
{
    [SerializeField] GameObject partical;
    [SerializeField] GameObject hedef;
    Int16 number_partical = 10;
    GameObject[] particals = new GameObject[10];

    float[,] best_matrix = new float[10, 3];
    float[] best_matrix_value = new float[10];
    float[] values = new float[10];
    float[,] values_ = new float[10, 3];

    float w = 0.95f;
    float c1 = 1.494f;
    float c2 = 1.494f;
    float delta_t = 0.001f;
    float[] gbest = new float[3];
    float val_gbest = 9999f;
    float value = 0;
    Vector3 hedef_last = new Vector3();





    void Start()
    {

        for (int i = 0; i < number_partical; i++)
        {
            GameObject partical_ = Instantiate(partical);
            partical_.transform.position = new Vector3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(1f, 9f), UnityEngine.Random.Range(-4f, 4f));
            particals[i] = partical_;
            
            particals[i].GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(0f, 20f), UnityEngine.Random.Range(0f, 20f), UnityEngine.Random.Range(0f, 20f));
            particals[i].GetComponent<Rigidbody>().mass = UnityEngine.Random.Range(0f, 20f);
        }

        for (int i = 0; i < number_partical; i++)
        {
            best_matrix[i, 0] = 0.0f;
            best_matrix[i, 1] = 0.0f;
            best_matrix[i, 2] = 0.0f;
            best_matrix_value[i] = 9999f;
            values[i] = 9999f;


        }


    }


    void Update()
    {

        if (w > 0.4) w = w - 0.001f;
        particalUpdate();
        if (hedef.transform.position != hedef_last)
        {
            for (int i = 0; i < number_partical; i++)
            {

                val_gbest = 9999f;
                value = 0;
                best_matrix_value[i] = 9999f;
                //particals[i].transform.position = new Vector3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(1f, 9f), UnityEngine.Random.Range(-4f, 4f));
                values[i] = 9999f;
                particals[i].GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-20, 20f), UnityEngine.Random.Range(-20, 20f));



            }
        }
        hedef_last = hedef.transform.position;

    }

    private void FixedUpdate()
    {
        

    }



    private void particalUpdate()
    {
        for (int i = 0; i < number_partical; i++)
        {


            float x_v = particals[i].GetComponent<Rigidbody>().velocity.x;
            float y_v = particals[i].GetComponent<Rigidbody>().velocity.y;
            float z_v = particals[i].GetComponent<Rigidbody>().velocity.z;

            float x = (w * x_v) + (c1 * UnityEngine.Random.Range(0f, 1f) * (best_matrix[i, 0] - particals[i].transform.position.x)) + (c2 * UnityEngine.Random.Range(0f, 1f) * (gbest[0] - particals[i].transform.position.x));

            float y = (w * y_v) + (c1 * UnityEngine.Random.Range(0f, 1f) * (best_matrix[i, 1] - particals[i].transform.position.y)) + (c2 * UnityEngine.Random.Range(0f, 1f) * (gbest[1] - particals[i].transform.position.y));

            float z = (w * z_v) + (c1 * UnityEngine.Random.Range(0f, 1f) * (best_matrix[i, 2] - particals[i].transform.position.z)) + (c2 * UnityEngine.Random.Range(0f, 1f) * (gbest[2] - particals[i].transform.position.z));


            particals[i].GetComponent<Rigidbody>().velocity = new Vector3(x, y, z);

            particals[i].transform.position = new Vector3(particals[i].transform.position.x + x * delta_t, particals[i].transform.position.y + y * delta_t, particals[i].transform.position.z + z * delta_t);

            value = amacfonksiyonu(particals[i].transform.position.x, particals[i].transform.position.y, particals[i].transform.position.z);
            
            
            values[i] = value;
            values_[i, 0] = particals[i].transform.position.x;
            values_[i, 1] = particals[i].transform.position.y;
            values_[i, 2] = particals[i].transform.position.z;


            if (best_matrix_value[i] > value)
            {
                best_matrix_value[i] = value;
                best_matrix[i, 0] = particals[i].transform.position.x;
                best_matrix[i, 1] = particals[i].transform.position.y;
                best_matrix[i, 2] = particals[i].transform.position.z;

            }

            if (val_gbest > values.Min())
            {
                int minIndex = Array.IndexOf(values, values.Min());
                gbest[0] = values_[minIndex, 0];
                gbest[1] = values_[minIndex, 1];
                gbest[2] = values_[minIndex, 2];
                val_gbest = values.Min();
            }



        }
    }


    private float amacfonksiyonu(float x, float y, float z)
    {

        return (x- hedef.transform.position.x) * (x - hedef.transform.position.x) +(y-hedef.transform.position.y)* (y - hedef.transform.position.y) + (z - hedef.transform.position.z) * (z - hedef.transform.position.z);
    }
}

    // public GameObject particlePrefab;

    // public int popsize = 20;// population size
    // public int MAXITER = 10;  // Maximum number of iterations

    // float gBestCost;
    // int bestParticle;
    // Vector3[] velocities;
    // Vector3[] positions;
    // Vector3[] pBestPositions;
    // float[] pBestCosts;
    // Vector3 gBestPosition;
    // GameObject[] particles;
    // public Transform target;
    // public float waitTime = 0.5f;
    // public float maxVelocity = 10;
    // public float startInertia = 0.7f;
    // public float endInertia = 0.2f;
    // private int iteration = 0;
    // public float c1 = 2;
    // public float c2 = 2;
    // Vector3[] waypoints;
    // Vector3 enemyPos;
    // float xMax;
    // float zMax;
    // GameObject enemy;
    // string direction = "";
    // bool firstChange = true;

    // // Start is called before the first frame update
    // void Start()
    // {

    //     particles = new GameObject[popsize];
    //     pBestCosts = new float[popsize];
    //     pBestPositions = new Vector3[popsize];
    //     velocities = new Vector3[popsize];
    //     positions = new Vector3[popsize];

    //     enemy = GameObject.FindGameObjectWithTag("Enemy");
    //     enemyPos = enemy.GetComponent<Transform>().position;
    //     target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    //     xMax = enemyPos.x;
    //     zMax = enemyPos.z;
    //     gBestPosition = enemyPos;
    //     initPopulation();
    //     StartCoroutine("RunPSO");
    // }

    // IEnumerator RunPSO()
    // {
    //     while (Vector3.Distance(enemyPos, target.position) >= 1)
    //     {
    //         Debug.Log("gBestCost " + gBestCost);

    //         while (iteration < MAXITER)
    //         {
    //             iteration++;
    //             for (int i = 0; i < popsize; i++)
    //             {
    //                 if (particles[i] == null)
    //                 {
    //                     continue;
    //                 }
    //                 Vector3 vel = Vector3.ClampMagnitude(getVelocity(velocities[i], positions[i], pBestPositions[i]), maxVelocity);//.normalized;
    //                 Vector3 pos = getPosition(positions[i], vel);
    //                 if (pos.x > xMax)
    //                 {
    //                     pos.x = xMax;
    //                 }
    //                 if (pos.z > zMax)
    //                 {
    //                     pos.z = zMax;
    //                 }
    //                 float cost = Vector3.Distance(target.position, pos);
    //                 if (cost < gBestCost)
    //                 {
    //                     gBestCost = cost;
    //                     bestParticle = i;
    //                     gBestPosition = pos;
    //                 }
    //                 if (cost < pBestCosts[i])
    //                 {
    //                     pBestCosts[i] = cost;
    //                     pBestPositions[i] = pos;
    //                 }
    //                 positions[i] = pos;
    //                 velocities[i] = vel;
    //                 particles[i].transform.position = pos;
    //             }
    //             if (iteration < MAXITER)
    //             {
    //                 yield return new WaitForSeconds(waitTime);
    //             }
    //             else
    //             {
    //                 Debug.Log("gBestPosition " + gBestPosition);
    //                 enemy.transform.position = gBestPosition;
    //                 yield return new WaitForSeconds(waitTime);
    //             }
    //         }
    //         enemyPos = gBestPosition;
    //         iteration = 0;
    //         clearPopulation();
    //         if (Vector3.Distance(enemyPos, target.position) >= 1)
    //         {
    //             initPopulation();
    //         }
    //     }
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     // Debug.DrawLine(enemy.position, target.position, Color.magenta);
    // }

    // Vector3 getVelocity(Vector3 previousVelocity, Vector3 previousPosition, Vector3 pBest)
    // {
    //     return getInertia() * previousVelocity + c1 * Random.Range(0f, 1f) * (pBest - previousPosition) + c2 * Random.Range(0f, 1f) * (gBestPosition - previousPosition);
    // }

    // Vector3 getPosition(Vector3 previousPosition, Vector3 currentVelocity)
    // {
    //     return previousPosition + currentVelocity;
    // }

    // float getInertia()
    // {
    //     return startInertia - ((startInertia - endInertia) * iteration / MAXITER);
    // }

    // void initPopulation()
    // {
    //     bool isOverlap = false;
    //     if (Physics2D.OverlapCircle(gBestPosition, 1))
    //     {
    //         isOverlap = true;
    //         if (direction.Equals(""))
    //         {
    //             if (firstChange)
    //             {
    //                 setRandomDirection();
    //             }
    //             else
    //             {
    //                 setDirection();
    //             }
    //         }
    //         else if (direction.Equals("h"))
    //         {
    //             xMax += 0.5f;
    //         }
    //         else if (direction.Equals("v"))
    //         {
    //             zMax += 0.5f;
    //         }
    //     }
    //     else
    //     {
    //         direction = "";
    //         xMax += 0.5f;
    //         zMax += 0.5f;
    //     }
    //     gBestCost = float.MaxValue;
    //     for (int i = 0; i < popsize; i++)
    //     {
    //         Vector3 pos = new Vector3(Random.Range(enemyPos.x - 0.5f, enemyPos.x + 0.5f), Random.Range(enemyPos.y - 0.5f, enemyPos.y + 0.5f));
    //         if (isOverlap)
    //         {
    //             pos = new Vector3(Random.Range(xMax - 1, xMax), Random.Range(zMax - 1, zMax));
    //         }
    //         particles[i] = Instantiate(particlePrefab, pos, Quaternion.identity);

    //         float cost = Vector3.Distance(target.position, pos);
    //         if (cost < gBestCost)
    //         {
    //             gBestCost = cost;
    //             bestParticle = i;
    //             gBestPosition = pos;
    //         }
    //         pBestPositions[i] = pos;
    //         pBestCosts[i] = cost;
    //         positions[i] = pos;
    //         velocities[i] = Vector3.ClampMagnitude(pos, maxVelocity);//.normalized;
    //     }
    // }

    // void clearPopulation()
    // {
    //     for (int i = 0; i < particles.Length; i++)
    //     {
    //         Destroy(particles[i]);
    //     }
    // }

    // void setDirection()
    // {
    //     Vector3 endCast = new Vector3();
    //     endCast.z = 35;
    //     endCast.z = enemyPos.z;
    //     RaycastHit2D hit =  GetHit(endCast);
    //     if (hit.collider == null || hit.distance>1)
    //     {
    //         direction = "h";
    //         xMax += 0.5f;
    //         return;
    //     }
    //     endCast.x = enemyPos.x;
    //     endCast.z = 15;
    //     hit = GetHit(endCast);
    //     if (hit.collider == null || hit.distance > 1)
    //     {
    //         direction = "v";
    //         zMax += 0.5f;
    //         return;
    //     }
    // }

    // void setRandomDirection()
    // {
    //     Vector3 endCast = new Vector3();
    //     RaycastHit2D hit;
    //     if (Random.Range(0f, 1f) > 0.5f)
    //     {
    //         endCast.z = 35;
    //         endCast.z = enemyPos.z;
    //         hit = GetHit(endCast);
    //         if (hit.collider == null || hit.distance > 1)
    //         {
    //             direction = "h";
    //             xMax += 0.5f;
    //         }
    //     }
    //     else
    //     {
    //         endCast.x = enemyPos.x;
    //         endCast.z = 15;
    //         hit = GetHit(endCast);
    //         if (hit.collider == null || hit.distance > 1)
    //         {
    //             direction = "v";
    //             zMax += 0.5f;
    //         }
    //     }
    //     firstChange = false;
    // }

    // RaycastHit2D GetHit(Vector3 endCast)
    // {
    //     return Physics2D.Linecast(enemyPos, endCast);
    // }

