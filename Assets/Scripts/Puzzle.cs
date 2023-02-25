using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Puzzle : MonoBehaviour
{
    private AbstractProcessor[] processors;
    private SelectionManager selectionManager;
    private TooltipManager tooltipManager;
    private CustomGrid<GameObject> grid;
    private IDictionary<Reciever, bool> recieverStatus;

    [SerializeField] private int gridHeight, gridWidth;
    [SerializeField] private float gridScale;
    [SerializeField] private Material gridMaterial;
    [SerializeField] private GameObject victoryCard;

    void Awake()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
        tooltipManager = FindObjectOfType<TooltipManager>();
        grid = new CustomGrid<GameObject>(gridHeight, gridWidth, gridScale);
        grid.RenderGrid(gridMaterial);
    }

    void Start()
    {
        processors = GetComponentsInChildren<AbstractProcessor>();
        foreach(AbstractProcessor p in processors)
        {
            grid.SetCellValue(p.transform.position, p.transform.parent.gameObject);
            grid.MakeImmutable(p.transform.position);
            p.enabled = true;
        }

        recieverStatus = new Dictionary<Reciever, bool> { };
        Reciever[] recievers = GetComponentsInChildren<Reciever>();
        foreach(Reciever r in recievers)
        {
            recieverStatus.Add(r, false);
        }
        drawRecieverTargets();
    }

    void OnEnable()
    {
        EventManager.Instance.RecieverSatisfied += HandleSatisfiedReciever;
        EventManager.Instance.RecieverUnsatisfied += HandleUnsatisfiedReciever;
    }

    void OnDisable()
    {
        EventManager.Instance.RecieverSatisfied -= HandleSatisfiedReciever;
        EventManager.Instance.RecieverUnsatisfied -= HandleUnsatisfiedReciever;
    }

    void HandleSatisfiedReciever(Reciever r)
    {
        recieverStatus[r] = true;

        bool flag = true;
        foreach(KeyValuePair<Reciever, bool> x in recieverStatus)
        {
            flag = flag && x.Value;
        }
        if (flag)
        {
            victoryCard.SetActive(true);
            this.enabled = false;
        }
    }

    void HandleUnsatisfiedReciever(Reciever r)
    {
        recieverStatus[r] = false;
    }

    private GameObject preview;

    void Update()
    {
        Destroy(preview);
        tooltipManager.ClearProcessorTooltips();
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if ((Physics.Raycast(ray, out RaycastHit hitData)) &&
            (grid.GetXY(hitData.point, out int x, out int y)))
        {
            GameObject value = grid.GetCellValue(hitData.point);

            if (value == null)
            {
                preview = drawPreview(hitData.point);
            }
            else
            {
                tooltipManager.CreateProcessorTooltip(value.GetComponentInChildren<AbstractProcessor>());
            }
        }
    }

    private GameObject drawPreview(Vector3 location)
    {
        GameObject selection = selectionManager.GetSelection();
        Vector3 rotation = selectionManager.GetRotation();
        Vector3 gridPos = grid.GetNearestGridPos(location);
        GameObject newPreview = Instantiate(selection, gridPos + new Vector3(0, 0.5f, 0), Quaternion.Euler(rotation));
        StandardShaderUtils.makeTransparent(newPreview);
        newPreview.transform.parent = this.transform;
        return newPreview;
    }

    private void drawRecieverTargets()
    {
        foreach(KeyValuePair<Reciever, bool> r in recieverStatus)
        {
            tooltipManager.drawRecieverTarget(r.Key);
        }
    }

    private void addProcessor(Vector3 position)
    {
        if (selectionManager.GetInventory(selectionManager.GetSelectionInt()) > 0)
        {
            EventManager.Instance.TriggerResetProcessors();

            GameObject selection = selectionManager.GetSelection();
            Vector3 rotation = selectionManager.GetRotation();
            Vector3 gridPos = grid.GetNearestGridPos(position);
            DestroyImmediate(preview);
            GameObject p = Instantiate(selection, gridPos + new Vector3(0, 0.5f, 0), Quaternion.Euler(rotation));
            p.GetComponentInChildren<AbstractProcessor>().enabled = true;
            p.transform.parent = this.transform;

            grid.SetCellValue(position, p);
            selectionManager.UpdateInventory(selectionManager.GetSelectionInt(), -1);

            EventManager.Instance.TriggerFireEmitters();
        }
    }

    private void deleteProcessor(Vector3 position)
    {
        GameObject oldObject = grid.GetCellValue(position);
        selectionManager.UpdateInventory(selectionManager.SelectionToInt(oldObject), 1);
        grid.SetCellValue(position, null);
        DestroyImmediate(oldObject);
        processors = GetComponentsInChildren<AbstractProcessor>();
        refresh();
    }

    private void refresh()
    {
        EventManager.Instance.TriggerResetProcessors();
        EventManager.Instance.TriggerFireEmitters();
    }

    public void OnConfirmAction(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hitData) && (!grid.IsImmutable(hitData.point)))
            {
                if (grid.GetCellValue(hitData.point) == null)
                {
                    addProcessor(hitData.point);
                }
                else
                {
                    deleteProcessor(hitData.point);
                }
            }
        }
    }
}
