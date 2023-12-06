import { Component } from '@angular/core';
import { StoreDTO } from '../../models/Store';
import { StoreService } from '../../services/store.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-stores',
  templateUrl: './stores.component.html',
  styleUrls: ['./stores.component.scss']
})
export class StoresComponent {

  stores: Array<StoreDTO> = [];

  storeForm!: FormGroup;
  showForm: boolean = false;
  isEdition: boolean = false;

  store!: StoreDTO;

  constructor(private storeService: StoreService,
  private fb:FormBuilder) {
    storeService.Stores$
      .subscribe(res => this.stores=res);
  }

  initForm() {
    this.storeForm = this.fb.group({
      branch: ['', [Validators.required]],
      address: ['', [Validators.required]],
    });
  }

  setFormVisibility = () => this.showForm = !this.showForm;

  addNew() {
    this.isEdition = false;
    this.setFormVisibility();
this.initForm();
  }

  isValidForm = (): boolean => this.storeForm.valid;

save() {
    if (this.isValidForm()) {
      this.storeService.insert(this.storeForm.value)
        .subscribe(res => {
          if (Response) {
            this.setFormVisibility();
          }
        });
    }
  }

  edit(store: StoreDTO) {
    this.store = store;
    this.isEdition = true;
    this.setFormVisibility();
    this.initForm();
    this.storeForm.patchValue(store);
  }

  update() {
if (this.isValidForm()) {
      this.storeService.update(this.store.id, this.storeForm.value)
        .subscribe(response => {
          if (Response) {
            this.setFormVisibility();
          }
        });
    }
  }

  delete(store: StoreDTO) {
    if (confirm(`¿Desea eliminar la tienda ${store.branch}?`)) {
this.storeService.delete(store.id)
        .subscribe(response => {
          if (Response) {
            alert("Registro eliminado");
          }
        });
    }
  }

}
