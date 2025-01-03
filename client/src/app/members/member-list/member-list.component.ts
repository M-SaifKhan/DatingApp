import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_Services/members.service';
import { MemberCardComponent } from "../member-card/member-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { AccountService } from '../../_Services/account.service';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent, PaginationModule, FormsModule, ButtonsModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  private accountService = inject(AccountService);
  memberService = inject(MembersService)
  genderList = [{value:'male', display:'Males'}, {value:'female', display:'Females'}]


  ngOnInit(): void {

    if (this.memberService.paginatedResult().items?.length == 0) this.loadMembers();
  }

  loadMembers() {
    this.memberService.getMembers()
  }
  resetFilters(){
    this.memberService.resetUserParams();
    this.loadMembers();
  }
  // pageChanged(event: any) {
  //   console.log("event worked")
  //   console.log(event.page)
  //   if (this.pageNumber !== event.page) {
  //     console.log("condition worked")
  //     this.pageNumber = event.pageNumber;
  //     this.pageSize = event.pageSize;
  //     this.loadMembers();
  //   }
  // }

  pageChanged(event: { page: number; itemsPerPage: number }): void {
    if (this.memberService.userParams().pageNumber !== event.page || this.memberService.userParams().pageSize !== event.itemsPerPage) {
      this.memberService.userParams().pageNumber = event.page;
      this.memberService.userParams().pageSize = event.itemsPerPage; 
      this.loadMembers(); 
    }
  }

}
