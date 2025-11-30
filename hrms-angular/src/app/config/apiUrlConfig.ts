// src/app/config/api-urls.ts
export const  API_URLS = {
    auth: {
    login: '/auth/login',
    register: '/auth/register'
  },
  roles: {
    getAll: '/roles',
    add: '/roles',
    update: (id: number) => `/roles/${id}`,
    delete: (id: number) => `/roles/${id}`,
    assignToUser: (userId: number, roleId: number) => `/users/${userId}/assign-role/${roleId}`
  },
  users: {
    getAll: '/users',
    add: '/users',
    update: (id: number) => `/users/${id}`,
    delete: (id: number) => `/users/${id}`
  },

   department: {
    getAll: '/department',
    create: '/department',
    update: (id: number) => `/department/${id}`,
    delete: (id: number) => `/department/${id}`
  },

  designation: {
    getAll: '/designation',
    create: '/designation',
    update: (id: number) => `/designation/${id}`,
    delete: (id: number) => `/designation/${id}`
  }
  // add more modules here like employees, departments, etc.
};
