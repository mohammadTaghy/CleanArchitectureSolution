import { Component, OnInit } from '@angular/core';
import { MatInput, MatInputModule } from '@angular/material/input'
import { MatToolbar } from '@angular/material/toolbar'
import { MatIcon } from '@angular/material/icon'

@Component({
  selector: 'app-main-page',
  templateUrl: './AdminPanel.component.html',
  styleUrls: ['./AdminPanel.component.css']
})
export class AdminPanelComponent implements OnInit {
  ngOnInit(): void {
    console.log("MainPageComponent");
  }

  isExpanded = false;
  element: HTMLElement;

  toggleActive(event: any) {
    debugger;
    event.preventDefault();
    if (this.element !== undefined) {
      this.element.style.backgroundColor = "white";
    }
    var target = event.currentTarget;
    target.style.backgroundColor = "#e51282";
    this.element = target;
  }

}
