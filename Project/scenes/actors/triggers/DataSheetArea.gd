class_name DataSheetArea extends Trigger

func _on_body_entered(body):
	GameManager.GetLevel().GetHUD()._open_data_sheet()
	
func _on_body_exited(body):
	GameManager.GetLevel().GetHUD()._close_data_sheet()
