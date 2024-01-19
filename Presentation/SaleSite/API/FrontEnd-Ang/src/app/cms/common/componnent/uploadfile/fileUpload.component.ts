import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component, Input, OnInit } from "@angular/core";

import { Subject } from "rxjs";
import { takeUntil } from 'rxjs/operators';

import { ApiAddresses } from "../../../../commonComponent/apiAddresses/apiAddresses.common";
import { CallAPIComponent } from "../../../../commonComponent/callAPI/callAPI.common";
import { BaseUIComponent } from "../baseUI.compnent";

@Component({
  selector: 'app-fileUpload',
  templateUrl: './fileUpload.component.html',

  styleUrls: ['./fileUpload.component.css']
})
export class FileUploadComponnent extends BaseUIComponent implements OnInit {
  //#region Properties
  uploadedMedia: Array<any> = [];
  isApiSetup = true;
  fileSizeUnit: number = 1024;
  //#endregion
  //#region input
  @Input() uploadUrl: string;
  @Input() isMultiFile: boolean;
  @Input() fileTypeCanChoose: string;
  //#ndregion
  //#region output

  //#endregion
  ngOnInit() { }
  constructor(private callAPIComponent: CallAPIComponent, private apiAddresses: ApiAddresses, private http: HttpClient) {
    super();
  }
  //#region Methods
  onFileBrowse(event: Event) {
    const target = event.target as HTMLInputElement;
    //console.log(event);
    this.processFiles(target.files);
  }
  processFiles(files) {
    for (const file of files) {
      var reader = new FileReader();
      reader.readAsDataURL(file); // read file as data url
      reader.onload = (event: any) => {
        // called once readAsDataURL is completed
        //console.log(file);
        //console.log(event);
        if (this.isMultiFile)
          this.uploadedMedia = [];
        this.uploadedMedia.push({
          FileName: file.name,
          FileSize:
            this.getFileSize(file.size) +
            ' ' +
            this.getFileSizeUnit(file.size),
          FileType: file.type,
          FileUrl: "",
          FileProgessSize: 0,
          FileProgress: 0,
          HasError: false,
          ErrorText: "",
          ngUnsubscribe: new Subject<any>(),
        });

        this.startProgress(file, this.uploadedMedia.length - 1);
      };
    }
  }

  async startProgress(file, indexParam) {
    //console.log("startProgress");
    let filteredFile = this.uploadedMedia
      .filter((u, index) => index === indexParam)
      .pop();
    //console.log(filteredFile);
    if (filteredFile != null) {
      let fileSize = this.getFileSize(file.size);
      let fileSizeInWords = this.getFileSizeUnit(file.size);
      if (this.isApiSetup) {
        let formData = new FormData();
        //console.log(file);
        formData.append('File', file);
        formData.append("FileType", "Image");
        //console.log("beforeSend");
        //console.log(formData);
        this.uploadMedia(formData)
          //.pipe(takeUntil(file.ngUnsubscribe))
          .subscribe(
            (res: any) => {
              if (res.status === 'progress') {
                let completedPercentage = parseFloat(res.message);
                filteredFile.FileProgessSize = `${(
                  (fileSize * completedPercentage) /
                  100
                ).toFixed(2)} ${fileSizeInWords}`;
               filteredFile.FileProgress = completedPercentage;
              } else if (res.isSuccess) {
               //console.log(res);

                filteredFile.FileUrl = res.result.fileAddresses[0];
                filteredFile.FileProgessSize = fileSize + ' ' + fileSizeInWords;
                filteredFile.FileProgress = 100;
                this.inputValue = this.uploadedMedia.map(p => p.FileUrl).join(';');
               //console.log(this.uploadedMedia);
               //console.log(indexParam);
               //console.log(res.result.fileAddresses[0]);
              }
              else {
                filteredFile.FileUrl = "";
                filteredFile.FileProgessSize = 0 + ' ' + fileSizeInWords;
                filteredFile.FileProgress = 0;
                filteredFile.HasError = true;
                filteredFile.ErrorText = res.errors;
              }
            },
            (error: any) => {
             //console.log('file upload error');
             //console.log(error);
            }
          );
      }
    }
  }
  fakeWaiter(ms: number) {
    return new Promise((resolve) => {
      setTimeout(resolve, ms);
    });
  }

  removeImage(idx: number) {
   //console.log(idx);
    this.uploadedMedia = this.uploadedMedia.filter((u, index) => index !== idx);
   //console.log(this.uploadedMedia);
  }
  getFileSize(fileSize: number): number {
    if (fileSize > 0) {
      if (fileSize < this.fileSizeUnit * this.fileSizeUnit) {
        fileSize = parseFloat((fileSize / this.fileSizeUnit).toFixed(2));
      } else if (
        fileSize <
        this.fileSizeUnit * this.fileSizeUnit * this.fileSizeUnit
      ) {
        fileSize = parseFloat(
          (fileSize / this.fileSizeUnit / this.fileSizeUnit).toFixed(2)
        );
      }
    }

    return fileSize;
  }
  getFileSizeUnit(fileSize: number) {
    let fileSizeInWords = 'bytes';

    if (fileSize > 0) {
      if (fileSize < this.fileSizeUnit) {
        fileSizeInWords = 'bytes';
      } else if (fileSize < this.fileSizeUnit * this.fileSizeUnit) {
        fileSizeInWords = 'KB';
      } else if (
        fileSize <
        this.fileSizeUnit * this.fileSizeUnit * this.fileSizeUnit
      ) {
        fileSizeInWords = 'MB';
      }
    }

    return fileSizeInWords;
  }
  uploadMedia(formData: any) {
    //console.log("Send File");
    return this.callAPIComponent.PostFile(formData, this.uploadUrl);

  }
  //#endregion
}
